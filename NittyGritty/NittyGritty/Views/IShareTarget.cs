using NittyGritty.Views.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Views
{
    public interface IShareTarget
    {
        event EventHandler ShareStarted;

        event ShareCompletedEventHandler ShareCompleted;

        event ShareFailedEventHandler ShareFailed;
    }
}
