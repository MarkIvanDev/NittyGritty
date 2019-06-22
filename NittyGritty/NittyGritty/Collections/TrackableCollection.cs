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
    public class TrackableCollection<TItem> : ITrackable<TItem>, ICollection<TItem>, ICollection, INotifyCollectionChanged, IDisposable
        where TItem : INotifyPropertyChanged
    {
        private NotifyCollectionChangedEventHandler _itemsChangedHandler;
        private SafeObservableCollection<TItem> _internalCollection = new SafeObservableCollection<TItem>();
        private readonly Dictionary<TItem, PropertyChangedEventHandler> _events =
            new Dictionary<TItem, PropertyChangedEventHandler>();

        public TrackableCollection() : this(new ObservableCollection<TItem>(), false)
        {
        }

        public TrackableCollection(IEnumerable<TItem> items) : this(items, false)
        {
        }

        public TrackableCollection(IEnumerable<TItem> items, bool trackItemChanges)
        {
            if (!(items is INotifyCollectionChanged collection))
            {
                throw new ArgumentException($"{items} must implement INotifyCollectionChanged", nameof(items));
            }

            Items = items.ToList();

            TrackItemChanges = trackItemChanges;
            TrackCollectionChanges = true;

            if (TrackItemChanges)
            {
                TrackItems();
            }

            _internalCollection.CollectionChanged += OnInternalCollectionChanged;
            _internalCollection.PropertyChanged += OnInternalPropertyChanged;

            IsTracking = true;
            Refresh();
        }

        /// <summary>Gets the original items source. </summary>
        protected IList<TItem> Items { get; private set; }

        #region Trackers

        private void OnOriginalCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            lock (SyncRoot)
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

        private void RegisterEvent(TItem item)
        {
            if (_events.ContainsKey(item))
                return;

            var handler = EventUtilities.RegisterEvent<TrackableCollection<TItem>, PropertyChangedEventHandler, PropertyChangedEventArgs>(
                this,
                h => item.PropertyChanged += h,
                h => item.PropertyChanged -= h,
                h => (o, e) => h(o, e),
                (subscriber, s, e) => subscriber.Refresh());

            _events.Add(item, handler);
        }

        private void DeregisterEvent(TItem item)
        {
            if (!_events.ContainsKey(item))
                return;

            var handler = _events[item];
            item.PropertyChanged -= handler;
            _events.Remove(item);
        }

        private void TrackCollection()
        {
            if (Items is INotifyCollectionChanged items)
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
                ((INotifyCollectionChanged)Items).CollectionChanged -= _itemsChangedHandler;
                _itemsChangedHandler = null;
            }
        }

        private void TrackItems()
        {
            foreach (var i in Items)
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
                _isTracking = value;
                if (value)
                {
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

        public virtual IEnumerable<TItem> GetItems()
        {
            return Items;
        }

        public void Refresh()
        {
            if (!IsTracking)
                return;

            lock (SyncRoot)
            {
                var list = GetItems();
                if (!_internalCollection.SequenceEqual(list))
                {
                    _internalCollection.Reset(list);
                }
            }
        }

        #endregion

        #region ICollection Implementation

        public int Count
        {
            get
            {
                lock (SyncRoot)
                {
                    return _internalCollection.Count;
                }
            }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public bool IsSynchronized
        {
            get { return true; }
        }

        private object _syncRoot;

        public object SyncRoot
        {
            get
            {
                return _syncRoot ?? (_syncRoot = new object());
            }
        }

        public void Add(TItem item)
        {
            Items.Add(item);
        }

        public void Clear()
        {
            Items.Clear();
        }

        public bool Contains(TItem item)
        {
            lock (SyncRoot)
            {
                return _internalCollection.Contains(item);
            }
        }

        public bool Remove(TItem item)
        {
            return Items.Remove(item);
        }

        public void CopyTo(TItem[] array, int arrayIndex)
        {
            lock (SyncRoot)
            {
                _internalCollection.CopyTo(array, arrayIndex);
            }
        }

        public void CopyTo(Array array, int index)
        {
            CopyTo((TItem[])array, index);
        }

        public IEnumerator<TItem> GetEnumerator()
        {
            lock (SyncRoot)
            {
                return _internalCollection.GetEnumerator();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            lock (SyncRoot)
            {
                return _internalCollection.GetEnumerator();
            }
        }

        #endregion

        #region Extra Collection Methods

        public void AddRange(IEnumerable<TItem> items)
        {
            var old = TrackCollectionChanges;
            TrackCollectionChanges = false;
            
            if (Items != null)
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

            if (Items != null)
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

        private void OnInternalCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(this, e);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnInternalPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            IsTracking = false;
            TrackCollectionChanges = false;
            TrackItemChanges = false;

            _internalCollection = null;
            Items = null;
        }

        #endregion
    }
}
