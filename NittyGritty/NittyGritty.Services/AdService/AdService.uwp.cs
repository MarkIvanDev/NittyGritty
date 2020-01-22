using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using NittyGritty.Platform.Store;

namespace NittyGritty.Services
{
    public partial class AdService
    {
        private const string AdTestId = "test";
        private const string ApplicationTestId_Banner = "3f83fe91-d6be-434d-a0ae-7351c5a997f1";
        private const string ApplicationTestId_Interstitial = "d25517cb-12d4-4699-8bdc-52040c712cab";
        private const string ApplicationTestId_Native = "d25517cb-12d4-4699-8bdc-52040c712cab";

        AdInfo PlatformGetAd(string key)
        {
            var ad = _adsByKey[key];
            if (UseTestValues)
            {
                switch (ad.Type)
                {
                    case AdInfo.AdType.Banner:
                        return new AdInfo(AdTestId, ad.Type, ApplicationTestId_Banner);
                    case AdInfo.AdType.InterstitialBanner:
                    case AdInfo.AdType.InterstitialVideo:
                        return new AdInfo(AdTestId, ad.Type, ApplicationTestId_Interstitial);
                    case AdInfo.AdType.Native:
                        return new AdInfo(AdTestId, ad.Type, ApplicationTestId_Native);
                    case AdInfo.AdType.Unknown:
                    default:
                        throw new InvalidOperationException("Ad is of unknown type");
                }
            }
            else
            {
                return ad;
            }
        }

        ReadOnlyCollection<AdInfo> PlatformGetAds(params string[] keys)
        {
            var ads = new Collection<AdInfo>();
            if(keys.Length == 0)
            {
                foreach (var key in _adsByKey.Keys)
                {
                    ads.Add(PlatformGetAd(key));
                }
            }
            else
            {
                foreach (var key in keys)
                {
                    ads.Add(PlatformGetAd(key));
                }
            }
            return new ReadOnlyCollection<AdInfo>(ads);
        }

        IInterstitialAd PlatformPrepareInterstitialAd(string key)
        {
            var ad = PlatformGetAd(key);
            return new InterstitialAd(ad);
        }

        INativeAdManager PlatformPrepareNativeAdManager(string key)
        {
            var ad = PlatformGetAd(key);
            return new NativeAdManager(ad);
        }
    }
}
