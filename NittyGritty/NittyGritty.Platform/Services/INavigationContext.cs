using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Platform
{
    public interface INavigationContext<T>
    {
        T Context { get; set; }
    }
}
