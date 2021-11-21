using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Services.Core
{
    public interface IResourceService<T> : IConfigurable<T>
    {
        T GetValue(string key);
    }
}
