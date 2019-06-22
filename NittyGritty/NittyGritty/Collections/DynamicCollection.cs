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
    public class DynamicCollection<TItem> : TrackableCollection<TItem>, ICollection<TItem>, IDynamic<TItem>
        where TItem : INotifyPropertyChanged
    {
        private ObservableCollection<TItem> _internalCollection = new ObservableCollection<TItem>();
        

        public DynamicCollection()
            : this(new ObservableCollection<TItem>())
        {
        }

        public DynamicCollection(IEnumerable<TItem> items)
            : this(items, null, null, true, false)
        {
        }

        public DynamicCollection(IEnumerable<TItem> items, Func<TItem, bool> filter)
            : this(items, filter, null, true, false)
        {
        }

        public DynamicCollection(IEnumerable<TItem> items, Func<TItem, bool> filter, Func<TItem, object> order)
            : this(items, filter, order, true, false)
        {
        }

        public DynamicCollection(IEnumerable<TItem> items, Func<TItem, bool> filter, Func<TItem, object> order, bool ascending)
            : this(items, filter, order, ascending, false)
        {
        }

        public DynamicCollection(IEnumerable<TItem> items, Func<TItem, bool> filter, Func<TItem, object> order, bool ascending, bool trackItemChanges)
            : base(items, trackItemChanges)
        {
            Filter = filter;
            Order = order;
            Ascending = ascending;
        }

        /// <summary>Gets the original items source. </summary>
        private IList<TItem> Items { get; set; }

        #region IDynamicCollection Implementation

        private int _limit;

        public int Limit
        {
            get { return _limit; }
            set
            {
                _limit = value;
                Refresh();
            }
        }

        private int _offset;

        public int Offset
        {
            get { return _offset; }
            set
            {
                _offset = value;
                Refresh();
            }
        }

        private bool _ascending;

        public bool Ascending
        {
            get { return _ascending; }
            set
            {
                _ascending = value;
                Refresh();
            }
        }

        private Func<TItem, bool> _filter;

        public Func<TItem, bool> Filter
        {
            get { return _filter; }
            set
            {
                _filter = value;
                Refresh();
            }
        }

        private Func<TItem, object> _order;

        public Func<TItem, object> Order
        {
            get { return _order; }
            set
            {
                _order = value;
                Refresh();
            }
        }

        public override IEnumerable<TItem> GetItems()
        {
            IEnumerable<TItem> items;

            if (Filter != null && Order != null && Ascending)
                items = Items.Where(Filter).OrderBy(Order);
            else if (Filter != null && Order != null && !Ascending)
                items = Items.Where(Filter).OrderByDescending(Order);
            else if (Filter == null && Order != null && Ascending)
                items = Items.OrderBy(Order);
            else if (Filter == null && Order != null && !Ascending)
                items = Items.OrderByDescending(Order);
            else if (Filter != null && Order == null)
                items = Items.Where(Filter);
            else if (Filter == null && Order == null)
                items = Items;
            else
                throw new Exception();

            if (Limit > 0 || Offset > 0)
                items = items.Skip(Offset).Take(Limit);

            return items;
        }

        #endregion

        #region ICollection Implementation

        public int Count
        {
            get
            {
                lock (SyncRoot)
                    return _internalCollection.Count;
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
            lock(SyncRoot)
            {
                return _internalCollection.Contains(item);
            }
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(TItem[] array, int arrayIndex)
        {
            lock(SyncRoot)
            {
                _internalCollection.CopyTo(array, arrayIndex);
            }
        }

        public bool Remove(TItem item)
        {
            return Items.Remove(item);
        }

        public IEnumerator<TItem> GetEnumerator()
        {
            lock(SyncRoot)
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

        #region INotifyCollectionChanged

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        #endregion

        #region IDisposable Implementation

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
