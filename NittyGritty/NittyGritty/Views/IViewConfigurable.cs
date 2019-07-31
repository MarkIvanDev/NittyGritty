using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Views
{
    public interface IViewConfigurable<T>
    {   
        void Configure(string key, T value);

        string GetKeyForValue(T value);
    }
}
