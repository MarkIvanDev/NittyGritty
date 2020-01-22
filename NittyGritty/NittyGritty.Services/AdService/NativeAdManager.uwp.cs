using System;
using System.Collections.Generic;
using System.Text;
using MSAd = Microsoft.Advertising.WinRT.UI;
using NittyGritty.Platform.Events;
using NittyGritty.Platform.Store;

namespace NittyGritty.Services
{
    public class NativeAdManager : INativeAdManager
    {
        public event EventHandler<NativeAdReadyEventArgs> AdReady;
        public event EventHandler<AdErrorEventArgs> ErrorOccurred;

        public NativeAdManager(AdInfo adInfo)
        {
            Context = new MSAd.NativeAdsManagerV2(adInfo.AppId, adInfo.Id);
            Context.AdReady += NativeAdsManager_AdReady;
            Context.ErrorOccurred += NativeAdsManager_ErrorOccurred;
        }

        public MSAd.NativeAdsManagerV2 Context { get; }

        private void NativeAdsManager_AdReady(object sender, MSAd.NativeAdReadyEventArgs e)
        {
            AdReady?.Invoke(this, new NativeAdReadyEventArgs(new NativeAd(e.NativeAd)));
        }

        private void NativeAdsManager_ErrorOccurred(object sender, MSAd.NativeAdErrorEventArgs e)
        {
            ErrorOccurred?.Invoke(this, new AdErrorEventArgs(e.ErrorMessage));
        }

        public void Request()
        {
            Context.RequestAd();
        }
    }
}
