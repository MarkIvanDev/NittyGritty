using NittyGritty.Views.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Views
{
    public interface IProtocolResult
    {
        event ProtocolResultCompletedEventHandler ProtocolResultCompleted;
    }
}
