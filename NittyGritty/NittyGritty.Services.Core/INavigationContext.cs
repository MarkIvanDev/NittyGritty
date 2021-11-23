using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Services.Core
{
    public interface INavigationContext<T>
    {
        T Context { get; set; }
    }
}
