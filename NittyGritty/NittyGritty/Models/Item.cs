using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Models
{
    public class Item<T> : ObservableObject
    {

        private T _data;

        public T Data
        {
            get { return _data; }
            set { Set(ref _data, value); }
        }

    }
}
