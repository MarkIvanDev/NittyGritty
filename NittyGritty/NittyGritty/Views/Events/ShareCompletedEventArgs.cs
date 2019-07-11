using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Views.Events
{
    public class ShareCompletedEventArgs : EventArgs
    {
        public ShareCompletedEventArgs()
        {

        }
    }

    public delegate void ShareCompletedEventHandler(object sender, ShareCompletedEventArgs e);
}
