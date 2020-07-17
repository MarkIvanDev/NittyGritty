using NittyGritty.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace NittyGritty.Collections
{
    public class TrackableCollection<TItem> : Collection<TItem>, ITrackable<TItem>, INotifyCollectionChanged, IDisposable
    {
        private NotifyCollectionChangedEventHandler _itemsChangedHandler;
        private readonly Dictionary<TItem, PropertyChangedEventHandler> _events =
            new Dictionary<TItem, PropertyChangedEventHandler>();
        private readonly object syncRoot = new object();

        public TrackableCollection() : this(Enumerable.Empty<TItem>(), false)
        {
        }

        public TrackableCollection(IEnumerable<TItem> items) : this(items, false)
        {
        }

        public TrackableCollection(IEnumerable<TItem> items, bool trackItemChanges) : base(items.ToList())
        {
            InternalCollection = new SafeObservableCollection<TItem>(items);

            TrackItemChanges = trackItemChanges;
            TrackCollectionChanges = true;

            IsTracking = true;
        }

        /// <summary>Gets the original items source. </summary>
        protected SafeObservableCollection<TItem> InternalCollection { get; private set; }

        #region Trackers

        private void OnOriginalCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            lock (syncRoot)
            {
                Refresh();

                if (TrackItemChanges)
                {
                    if (e.Action == NotifyCollectionChangedAction.Reset)
                    {
                        UntrackItems();
                        TrackItems();
                    }
                    else
                    {
                        if (e.NewItems != null)
                        {
                            foreach (var item in e.NewItems.OfType<TItem>())
                                RegisterEvent(item);
                        }

                        if (e.OldItems != null)
                        {
                            foreach (var item in e.OldItems.OfType<TItem>())
                                DeregisterEvent(item);
                        }
                    }
                }
            }
        }

        private void OnCollectionItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            lock (syncRoot)
            {
                Refresh();

                OnItemPropertyChanged(sender, e);
            }
        }

        private void RegisterEvent(TItem item)
        {
            if (_events.ContainsKey(item))
                return;

            if(item is INotifyPropertyChanged i)
            {
                var handler = EventUtilities.RegisterEvent<TrackableCollection<TItem>, PropertyChangedEventHandler, PropertyChangedEventArgs>(
                this,
                h => i.PropertyChanged += h,
                h => i.PropertyChanged -= h,
                h => (o, e) => h(o, e),
                (subscriber, s, e) => subscriber.OnCollectionItemPropertyChanged(s, e));

                _events.Add(item, handler);
            }
        }

        private void DeregisterEvent(TItem item)
        {
            if (!_events.ContainsKey(item))
                return;

            if(item is INotifyPropertyChanged i)
            {
                var handler = _events[item];
                i.PropertyChanged -= handler;
                _events.Remove(item);
            }
        }

        private void TrackCollection()
        {
            if (InternalCollection is INotifyCollectionChanged items)
            {
                var collection = items;
                _itemsChangedHandler = EventUtilities.RegisterEvent<TrackableCollection<TItem>, NotifyCollectionChangedEventHandler, NotifyCollectionChangedEventArgs>(
                    this,
                    h => collection.CollectionChanged += h,
                    h => collection.CollectionChanged -= h,
                    h => (o, e) => h(o, e),
                    (subscriber, s, e) => subscriber.OnOriginalCollectionChanged(s, e));
            }
        }

        private void UntrackCollection()
        {
            if (_itemsChangedHandler != null)
            {
                if(InternalCollection is INotifyCollectionChanged items)
                {
                    items.CollectionChanged -= _itemsChangedHandler;
                }
                _itemsChangedHandler = null;
            }
        }

        private void TrackItems()
        {
            foreach (var i in InternalCollection)
            {
                RegisterEvent(i);
            }
        }

        private void UntrackItems()
        {
            foreach (var item in _events.Keys.ToArray())
            {
                DeregisterEvent(item);
            }
        }
        #endregion

        #region ITrackable

        private bool _isTracking;

        /// <summary>Gets or sets a flag whether the view should automatically be updated when needed. 
        /// Disable this flag when doing multiple of operations on the underlying collection. 
        /// Enabling this flag automatically updates the view if needed. </summary>
        public bool IsTracking
        {
            get { return _isTracking; }
            set
            {
                if (value != _isTracking)
                {
                    _isTracking = value;
                    OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(IsTracking)));
                    Refresh(); 
                }
            }
        }

        private bool _trackCollectionChanges;

        /// <summary>Gets or sets a value indicating whether the view should listen for collection 
        /// changed events on the underlying collection (default: true). </summary>
        public bool TrackCollectionChanges
        {
            get { return _trackCollectionChanges; }
            set
            {
                if (value != _trackCollectionChanges)
                {
                    _trackCollectionChanges = value;
                    OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(TrackCollectionChanges)));
                    if (_trackCollectionChanges)
                    {
                        TrackCollection();
                    }
                    else
                    {
                        UntrackCollection();
                    }

                    Refresh();
                }
            }
        }

        private bool _trackItemChanges;

        public bool TrackItemChanges
        {
            get { return _trackItemChanges; }
            set
            {
                if (value != _trackItemChanges)
                {
                    _trackItemChanges = value;
                    OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(TrackItemChanges)));
                    if (_trackItemChanges)
                    {
                        TrackItems();
                    }
                    else
                    {
                        UntrackItems();
                    }

                    Refresh();
                }
            }
        }

        public virtual IList<TItem> GetItems()
        {
            return InternalCollection;
        }

        public void Refresh()
        {
            if (!IsTracking)
                return;

            lock (syncRoot)
            {
                var list = GetItems();
                if (!Items.SequenceEqual(list))
                {
                    Reset(list);
                }
            }
        }

        public void Reset(IList<TItem> list)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            Items.Clear();
            foreach (var i in list)
            {
                Items.Add(i);
            }

            OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(Count)));
            OnCollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        #endregion

        #region Override Collection Methods
        protected override void ClearItems()
        {
            InternalCollection.Clear();
        }

        protected override void InsertItem(int index, TItem item)
        {
            InternalCollection.Insert(index, item);
        }

        protected override void RemoveItem(int index)
        {
            InternalCollection.RemoveAt(index);
        }

        protected override void SetItem(int index, TItem item)
        {
            InternalCollection[index] = item;
        }
        #endregion

        #region Extra Collection Methods

        public void AddRange(IEnumerable<TItem> items)
        {
            var old = TrackCollectionChanges;
            TrackCollectionChanges = false;
            
            if (InternalCollection != null)
            {
                foreach (var item in items)
                {
                    Add(item);
                }
            }

            TrackCollectionChanges = old;
        }

        public void RemoveRange(IEnumerable<TItem> items)
        {
            var old = TrackCollectionChanges;
            TrackCollectionChanges = false;

            if (InternalCollection != null)
            {
                foreach (var item in items)
                {
                    Remove(item);
                }
            }

            TrackCollectionChanges = old;
        }

        #endregion

        #region INotifyChanged - Property and Collection

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(this, e);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        public event PropertyChangedEventHandler ItemPropertyChanged;

        private void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ItemPropertyChanged?.Invoke(this, e);
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            IsTracking = false;
            TrackCollectionChanges = false;
            TrackItemChanges = false;
            
            InternalCollection = null;
        }

        #endregion
    }
}
