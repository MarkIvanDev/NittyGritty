using System;
using System.Collections.Generic;
using System.Text;
using NittyGritty.Platform.Events;
using NittyGritty.Platform.Store;
using MSAd = Microsoft.Advertising.WinRT.UI;

namespace NittyGritty.Services
{
    public class InterstitialAd : IInterstitialAd
    {
        private AdInfo adInfo;

        public event EventHandler AdReady;
        public event EventHandler<AdErrorEventArgs> ErrorOccurred;
        public event EventHandler Completed;
        public event EventHandler Cancelled;

        public InterstitialAd(AdInfo adInfo)
        {
            this.adInfo = adInfo;
            Context = new MSAd.InterstitialAd();
            Context.AdReady += InterstitialAd_AdReady;
            Context.ErrorOccurred += InterstitialAd_ErrorOccurred;
            Context.Completed += InterstitialAd_Completed;
            Context.Cancelled += InterstitialAd_Cancelled;
        }

        public MSAd.InterstitialAd Context { get; }

        private void InterstitialAd_AdReady(object sender, object e)
        {
            AdReady?.Invoke(this, new EventArgs());
        }

        private void InterstitialAd_ErrorOccurred(object sender, MSAd.AdErrorEventArgs e)
        {
            ErrorOccurred?.Invoke(this, new AdErrorEventArgs(e.ErrorMessage));
        }

        private void InterstitialAd_Completed(object sender, object e)
        {
            Completed?.Invoke(this, new EventArgs());
        }

        private void InterstitialAd_Cancelled(object sender, object e)
        {
            Cancelled?.Invoke(this, new EventArgs());
        }

        public void Close()
        {
            Context.Close();
        }

        public void Request()
        {
            switch (adInfo.Type)
            {
                case AdInfo.AdType.InterstitialBanner:
                    Context.RequestAd(MSAd.AdType.Display, adInfo.AppId, adInfo.Id);
                    break;
                case AdInfo.AdType.InterstitialVideo:
                    Context.RequestAd(MSAd.AdType.Video, adInfo.AppId, adInfo.Id);
                    break;
                case AdInfo.AdType.Native:
                case AdInfo.AdType.Banner:
                case AdInfo.AdType.Unknown:
                default:
                    throw new InvalidOperationException("Ad is not an interstitial ad");
            }
        }

        public void Show()
        {
            Context.Show();
        }
    }
}
