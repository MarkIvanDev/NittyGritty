using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Models
{
    public class SelectableItem<T> : ObservableObject
    {

        private bool? _isSelected;

        public bool? IsSelected
        {
            get { return _isSelected; }
            set { Set(ref _isSelected, value); }
        }

        private T _item;

        public T Item
        {
            get { return _item; }
            set { Set(ref _item, value); }
        }

    }
}
