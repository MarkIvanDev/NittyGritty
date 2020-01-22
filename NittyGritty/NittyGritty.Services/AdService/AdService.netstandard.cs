using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using NittyGritty.Platform.Store;

namespace NittyGritty.Services
{
    public partial class AdService
    {
        AdInfo PlatformGetAd(string key)
        {
            throw new NotImplementedException();
        }

        ReadOnlyCollection<AdInfo> PlatformGetAds(params string[] keys)
        {
            throw new NotImplementedException();
        }

        IInterstitialAd PlatformPrepareInterstitialAd(string key)
        {
            throw new NotImplementedException();
        }

        INativeAdManager PlatformPrepareNativeAdManager(string key)
        {
            throw new NotImplementedException();
        }
    }
}
