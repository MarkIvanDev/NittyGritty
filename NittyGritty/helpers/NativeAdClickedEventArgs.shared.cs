using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Platform.Events
{
    public class NativeAdClickedEventArgs : EventArgs
    {
        public NativeAdClickedEventArgs(string requestId)
        {
            RequestId = requestId;
        }

        public string RequestId { get; }
    }
}
