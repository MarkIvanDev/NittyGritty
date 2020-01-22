using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using NittyGritty.Platform.Store;

namespace NittyGritty.Services
{
    public interface IAdService
    {
        bool UseTestValues { get; }

        AdInfo GetAd(string key);

        ReadOnlyCollection<AdInfo> GetAds(params string[] keys);

        IInterstitialAd PrepareInterstitialAd(string key);

        INativeAdManager PrepareNativeAdManager(string key);
    }
}
