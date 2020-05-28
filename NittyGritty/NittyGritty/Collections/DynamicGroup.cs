using NittyGritty.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Collections
{
    public class DynamicGroup<TKey, TItem>
    {
        public DynamicGroup(TKey key) : this(key, new DynamicCollection<TItem>())
        {

        }

        public DynamicGroup(TKey key, IList<TItem> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            Key = key;
            Items = items is DynamicCollection<TItem> dynamicCollection ?
                dynamicCollection :
                new DynamicCollection<TItem>(items);
        }

        public TKey Key { get; }

        public DynamicCollection<TItem> Items { get; }
    }
}
