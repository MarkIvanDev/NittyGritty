using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Views.Events
{
    public class ShareFailedEventArgs : EventArgs
    {
        public ShareFailedEventArgs(string error)
        {
            Error = error;
        }

        public string Error { get; }
    }

    public delegate void ShareFailedEventHandler(object sender, ShareFailedEventArgs e);
}
