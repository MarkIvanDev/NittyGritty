using System;
using System.Collections.Generic;
using System.Text;
using NittyGritty.Platform.Store;

namespace NittyGritty.Platform.Events
{
    public class NativeAdReadyEventArgs : EventArgs
    {
        public NativeAdReadyEventArgs(INativeAd nativeAd)
        {
            NativeAd = nativeAd;
        }

        public INativeAd NativeAd { get; }
    }
}
