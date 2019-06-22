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
            IEnumerable<TItem> items = Items;

            //if (Filter != null && Order != null && Ascending)
            //    items = Items.Where(Filter).OrderBy(Order);
            //else if (Filter != null && Order != null && !Ascending)
            //    items = Items.Where(Filter).OrderByDescending(Order);
            //else if (Filter == null && Order != null && Ascending)
            //    items = Items.OrderBy(Order);
            //else if (Filter == null && Order != null && !Ascending)
            //    items = Items.OrderByDescending(Order);
            //else if (Filter != null && Order == null)
            //    items = Items.Where(Filter);
            //else if (Filter == null && Order == null)
            //    items = Items;
            //else
            //    throw new Exception();

            if(Filter != null)
            {
                items = items.Where(Filter);
            }

            if(Order != null)
            {
                items = Ascending ? items.OrderBy(Order) : items.OrderByDescending(Order);
            }

            if (Limit > 0 || Offset > 0)
            {
                items = items.Skip(Offset).Take(Limit);
            }

            return items;
        }

        #endregion

    }
}
