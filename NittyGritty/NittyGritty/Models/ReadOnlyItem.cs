using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Models
{
    public class ReadOnlyItem<T>
    {
        public ReadOnlyItem(T data)
        {
            Data = data;
        }

        public T Data { get; }

        public static implicit operator ReadOnlyItem<T>(T data) => new ReadOnlyItem<T>(data);

        public static implicit operator T(ReadOnlyItem<T> item) => item.Data;

    }
}
