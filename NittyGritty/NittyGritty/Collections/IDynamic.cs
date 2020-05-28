using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Collections
{
    public interface IDynamic<TItem>
    {
        /// <summary>
        /// Gets or sets the maximum number of items in the view.
        /// </summary>
        int Limit { get; set; }

        /// <summary>
        /// Gets or sets the offset from where the results a selected.
        /// </summary>
        int Offset { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to sort ascending or descending.
        /// </summary>
        bool Ascending { get; set; }

        /// <summary>
        /// Gets or sets the filter (a Func{TItem, bool} object).
        /// </summary>
        Func<TItem, bool> Filter { get; set; }

        /// <summary>
        /// Gets or sets the order (a Func{TItem, object} object).
        /// </summary>
        Func<TItem, object> Order { get; set; }

        /// <summary>
        /// Gets or sets the comparer (an IComparer{TItem} object)
        /// If comparer is set, the Order property is not used.
        /// </summary>
        IComparer<TItem> Comparer { get; set; }
    }
}
