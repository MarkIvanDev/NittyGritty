using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Services
{
    public interface INavigationContext<T>
    {
        T Context { get; set; }
    }
}
