using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NittyGritty.Views.Events
{
    public class ProtocolResultCompletedEventArgs : EventArgs
    {
        public ProtocolResultCompletedEventArgs(IDictionary<string, object> result)
        {
            Result = new ReadOnlyDictionary<string, object>(result);
        }

        public ReadOnlyDictionary<string, object> Result { get; }
    }

    public delegate void ProtocolResultCompletedEventHandler(object sender, ProtocolResultCompletedEventArgs e);
}
