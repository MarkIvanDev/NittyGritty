using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Extensions
{
    public static class EnumerableExtensions
    {
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> additional)
        {
            foreach (var item in additional)
            {
                collection.Add(item);
            }
        }
    }
}
