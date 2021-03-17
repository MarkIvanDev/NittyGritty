using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Extensions
{
    public static class EnumerableExtensions
    {
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> range)
        {
            foreach (var item in range)
            {
                collection.Add(item);
            }
        }

        public static void RemoveRange<T>(this ICollection<T> collection, IEnumerable<T> range)
        {
            foreach (var item in range)
            {
                collection.Remove(item);
            }
        }

        public static void ClearAll(this IList list)
        {
            foreach (var item in list)
            {
                if (item is IList itemList)
                {
                    itemList.ClearAll();
                }
            }
            list.Clear();
        }
    }
}
