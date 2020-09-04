using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Services
{
    public interface IResourceService<T> : IConfigurable<T>
    {
        T GetValue(string key);
    }
}
