using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Services
{
    public interface IConfigurable<T>
    {
        void Configure(string key, T value);

        string GetKeyForValue(T value);
    }
}
