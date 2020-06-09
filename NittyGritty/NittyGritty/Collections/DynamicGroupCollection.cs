using NittyGritty.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace NittyGritty.Collections
{
    public class DynamicGroupCollection<TKey, TItem> : Collection<DynamicGroup<TKey, TItem>>, IDynamic<TItem>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        private readonly object synchronizationObject = new object();

        #region Dynamic

        private int _limit;

        public int Limit
        {
            get { return _limit; }
            set
            {
                lock (synchronizationObject)
                {
                    _limit = value;
                    foreach (var item in this)
                    {
                        item.Items.Limit = value;
                    }
                }
            }
        }
        
        private int _offset;

        public int Offset
        {
            get { return _offset; }
            set
            {
                lock (synchronizationObject)
                {
                    _offset = value;
                    foreach (var item in this)
                    {
                        item.Items.Offset = value;
                    }
                }
            }
        }

        private bool _ascending;

        public bool Ascending
        {
            get { return _ascending; }
            set
            {
                lock (synchronizationObject)
                {
                    _ascending = value;
                    foreach (var item in this)
                    {
                        item.Items.Ascending = value;
                    }
                    var sorted = _ascending ? this.OrderBy(i => i.Key) : this.OrderByDescending(i => i.Key);
                    Reset(sorted.ToList());
                }
            }
        }

        private Func<TItem, bool> _filter;

        public Func<TItem, bool> Filter
        {
            get { return _filter; }
            set
            {
                lock (synchronizationObject)
                {
                    _filter = value;
                    foreach (var item in this)
                    {
                        item.Items.Filter = value;
                    }
                }
            }
        }

        private Func<TItem, object> _order;

        public Func<TItem, object> Order
        {
            get { return _order; }
            set
            {
                lock (synchronizationObject)
                {
                    _order = value;
                    foreach (var item in this)
                    {
                        item.Items.Order = value;
                    }
                }
            }
        }

        private IComparer<TItem> _comparer;

        public IComparer<TItem> Comparer
        {
            get { return _comparer; }
            set
            {
                lock (synchronizationObject)
                {
                    _comparer = value;
                    foreach (var item in this)
                    {
                        item.Items.Comparer = value;
                    }
                }
            }
        }

        #endregion

        #region INotifyChanged - Property and Collection

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(this, e);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        #endregion

        #region Collection Overrides
        public void Reset(IList<DynamicGroup<TKey, TItem>> items)
        {
            ResetItems(items);
        }

        public void Move(int oldIndex, int newIndex)
        {
            MoveItem(oldIndex, newIndex);
        }

        public void Replace(int index, DynamicGroup<TKey, TItem> item)
        {
            SetItem(index, item);
        }

        protected override void InsertItem(int index, DynamicGroup<TKey, TItem> item)
        {
            item.Items.Limit = Limit;
            item.Items.Offset = Offset;
            item.Items.Ascending = Ascending;
            item.Items.Filter = Filter;
            item.Items.Order = Order;
            item.Items.Comparer = Comparer;
            base.InsertItem(index, item);
            OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(Count)));
            OnCollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
        }

        protected override void RemoveItem(int index)
        {
            var removedItem = this[index];
            base.RemoveItem(index);
            OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(Count)));
            OnCollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removedItem, index));
        }

        protected override void SetItem(int index, DynamicGroup<TKey, TItem> item)
        {
            var originalItem = this[index];
            item.Items.Limit = Limit;
            item.Items.Offset = Offset;
            item.Items.Ascending = Ascending;
            item.Items.Filter = Filter;
            item.Items.Order = Order;
            item.Items.Comparer = Comparer;
            base.SetItem(index, item);
            OnCollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, item, originalItem, index));
        }

        protected virtual void MoveItem(int oldIndex, int newIndex)
        {
            var removedItem = this[oldIndex];
            base.RemoveItem(oldIndex);
            base.InsertItem(newIndex, removedItem);
            OnCollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Move, removedItem, newIndex, oldIndex));
        }

        protected override void ClearItems()
        {
            base.ClearItems();
            OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(Count)));
            OnCollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        protected virtual void ResetItems(IList<DynamicGroup<TKey, TItem>> items)
        {
            base.ClearItems();
            foreach (var item in items)
            {
                item.Items.Limit = Limit;
                item.Items.Offset = Offset;
                item.Items.Ascending = Ascending;
                item.Items.Filter = Filter;
                item.Items.Order = Order;
                item.Items.Comparer = Comparer;
                base.InsertItem(Items.Count, item);
            }
            OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(Count)));
            OnCollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        #endregion
    }
}
