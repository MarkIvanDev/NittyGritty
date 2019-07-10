using NittyGritty.Views.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Views
{
    public interface IFileOpenPicker
    {
        event PickFileChangedEventHandler PickFileChanged;
    }

}
