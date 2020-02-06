using System;
using System.Collections.Generic;
using System.Text;
using NittyGritty.Platform.Events;

namespace NittyGritty.Platform.Store
{
    public interface INativeAdManager
    {
        event EventHandler<NativeAdReadyEventArgs> AdReady;

        event EventHandler<AdErrorEventArgs> ErrorOccurred;

        void Request();
    }

}
