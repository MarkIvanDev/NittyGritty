using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Views
{
    public interface INavigationContext<T>
    {
        T Context { get; set; }
    }
}
