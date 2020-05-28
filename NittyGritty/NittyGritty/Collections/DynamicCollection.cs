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
    public class DynamicCollection<TItem> : TrackableCollection<TItem>, IDynamic<TItem>
        // where TItem : INotifyPropertyChanged
    {
        
        public DynamicCollection()
            : this(Enumerable.Empty<TItem>())
        {
        }

        public DynamicCollection(IEnumerable<TItem> items)
            : this(items, null, (Func<TItem, object>)null, true, false)
        {
        }

        public DynamicCollection(IEnumerable<TItem> items, Func<TItem, bool> filter)
            : this(items, filter, (Func<TItem, object>)null, true, false)
        {
        }

        public DynamicCollection(IEnumerable<TItem> items, Func<TItem, bool> filter, Func<TItem, object> order)
            : this(items, filter, order, true, false)
        {
        }

        public DynamicCollection(IEnumerable<TItem> items, Func<TItem, bool> filter, IComparer<TItem> comparer)
            : this(items, filter, comparer, true, false)
        {
        }

        public DynamicCollection(IEnumerable<TItem> items, Func<TItem, bool> filter, Func<TItem, object> order, bool ascending)
            : this(items, filter, order, ascending, false)
        {
        }

        public DynamicCollection(IEnumerable<TItem> items, Func<TItem, bool> filter, IComparer<TItem> comparer, bool ascending)
            : this(items, filter, comparer, ascending, false)
        {
        }

        public DynamicCollection(IEnumerable<TItem> items, Func<TItem, bool> filter, Func<TItem, object> order, bool ascending, bool trackItemChanges)
            : base(items, trackItemChanges)
        {
            Filter = filter;
            Order = order;
            Ascending = ascending;
        }

        public DynamicCollection(IEnumerable<TItem> items, Func<TItem, bool> filter, IComparer<TItem> comparer, bool ascending, bool trackItemChanges)
            : base(items, trackItemChanges)
        {
            Filter = filter;
            Comparer = comparer;
            Ascending = ascending;
        }

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

        private IComparer<TItem> _comparer;

        public IComparer<TItem> Comparer
        {
            get { return _comparer; }
            set
            {
                _comparer = value;
                Refresh();
            }
        }

        public override IList<TItem> GetItems()
        {
            var list = (IEnumerable<TItem>)InternalCollection;

            if (!(Filter is null))
            {
                list = list.Where(Filter);
            }

            if (!(Comparer is null))
            {
                list = Ascending ?
                    list.OrderBy(i => i, Comparer) :
                    list.OrderByDescending(i => i, Comparer);
            }
            else if (!(Order is null))
            {
                list = Ascending ?
                    list.OrderBy(Order) :
                    list.OrderByDescending(Order);
            }

            if (Offset > 0)
            {
                list = list.Skip(Offset);
            }

            if (Limit > 0)
            {
                list = list.Take(Limit);
            }

            return list.ToList();
        }

        #endregion

    }
}
