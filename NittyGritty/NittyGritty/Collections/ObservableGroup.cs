using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NittyGritty.Collections
{
    public class ObservableGroup<TKey, TItem>
    {
        public ObservableGroup(TKey key) : this(key, new ObservableCollection<TItem>())
        {

        }

        public ObservableGroup(TKey key, IList<TItem> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            Key = key;
            Items = new ObservableCollection<TItem>(items);
        }

        public TKey Key { get; }

        public ObservableCollection<TItem> Items { get; }
    }
}
