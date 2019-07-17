using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NittyGritty.Views.Events
{
    public class ProtocolResultCompletedEventArgs : EventArgs
    {
        public ProtocolResultCompletedEventArgs(IDictionary<string, string> result)
        {
            Result = new ReadOnlyDictionary<string, string>(result);
        }

        public ReadOnlyDictionary<string, string> Result { get; }
    }

    public delegate void ProtocolResultCompletedEventHandler(object sender, ProtocolResultCompletedEventArgs e);
}
