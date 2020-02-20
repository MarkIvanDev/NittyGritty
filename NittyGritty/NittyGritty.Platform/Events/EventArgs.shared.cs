using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Platform.Events
{
    public class EventArgs<T> : EventArgs
    {
        public EventArgs(T data)
        {
            Data = data;
        }

        public T Data { get; }
    }
}
