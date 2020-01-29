using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using NittyGritty.Platform.Store;

namespace NittyGritty.Services
{
    public partial class AdService : IAdService, IConfigurable<AdInfo>
    {
        public AdService(bool useTestValues)
        {
            UseTestValues = useTestValues;
        }

        #region IConfigurable
        private readonly Dictionary<string, AdInfo> _adsByKey = new Dictionary<string, AdInfo>();

        public void Configure(string key, AdInfo value)
        {
            lock (_adsByKey)
            {
                if (_adsByKey.ContainsKey(key))
                {
                    throw new ArgumentException($"This key is already used: {key}");
                }

                if (_adsByKey.Any(p => p.Value == value))
                {
                    throw new ArgumentException(
                        "This ad is already configured with key " + _adsByKey.First(p => p.Value == value).Key);
                }

                _adsByKey.Add(
                    key,
                    value);
            }
        }

        public string GetKeyForValue(AdInfo value)
        {
            lock (_adsByKey)
            {
                if (_adsByKey.ContainsValue(value))
                {
                    return _adsByKey.FirstOrDefault(p => p.Value == value).Key;
                }
                else
                {
                    throw new ArgumentException($"The ad '{value.Id}' is unknown by the AdService.");
                }
            }
        }
        #endregion

        public bool UseTestValues { get; }

        public AdInfo GetAd(string key)
        {
            return PlatformGetAd(key);
        }

        public ReadOnlyCollection<AdInfo> GetAds(params string[] keys)
        {
            return PlatformGetAds(keys);
        }

        public IInterstitialAd PrepareInterstitialAd(string key)
        {
            return PlatformPrepareInterstitialAd(key);
        }

        public INativeAdManager PrepareNativeAdManager(string key)
        {
            return PlatformPrepareNativeAdManager(key);
        }
    }
}
