using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace NittyGritty.Collections
{
    public interface ITrackable<TItem> where TItem : INotifyPropertyChanged
    {
        bool IsTracking { get; set; }

        bool TrackCollectionChanges { get; set; }

        bool TrackItemChanges { get; set; }

        IEnumerable<TItem> GetItems();

        void Refresh();
    }
}
