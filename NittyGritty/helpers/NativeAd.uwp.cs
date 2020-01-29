using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using NittyGritty.Models;
using NittyGritty.Platform.Events;
using NittyGritty.Platform.Store;
using Windows.UI.Xaml;
using MSAd = Microsoft.Advertising.WinRT.UI;

namespace NittyGritty.Services
{
    public class NativeAd : INativeAd
    {
        public event EventHandler<NativeAdClickedEventArgs> AdClicked;

        public NativeAd(MSAd.NativeAdV2 nativeAd)
        {
            Context = nativeAd;
            Title = nativeAd.Title;
            Description = nativeAd.Description;
            CallToActionText = nativeAd.CallToActionText;
            SponsoredBy = nativeAd.SponsoredBy;
            Price = nativeAd.Price;
            PrivacyUrl = nativeAd.PrivacyUrl;
            Rating = nativeAd.Rating;
            AdIcon = new ImageInfo(nativeAd.AdIcon.BaseUri, nativeAd.AdIcon.Height, nativeAd.AdIcon.Width);
            IconImage = new ImageInfo(new Uri(nativeAd.IconImage.Url), nativeAd.IconImage.Height, nativeAd.IconImage.Width);
            Images = new ReadOnlyCollection<ImageInfo>(nativeAd.MainImages.Select(i => new ImageInfo(new Uri(i.Url), i.Height, i.Width)).ToList());
            AdditionalAssets = nativeAd.AdditionalAssets;
            Context.AdClicked += NativeAd_AdClicked;
        }

        private void NativeAd_AdClicked(object sender, MSAd.NativeAdClickedEventArgs e)
        {
            AdClicked?.Invoke(this, new NativeAdClickedEventArgs(e.RequestId));
        }

        public MSAd.NativeAdV2 Context { get; }

        public string Title { get; }

        public string Description { get; }

        public string CallToActionText { get; }

        public string SponsoredBy { get; }

        public string Price { get; }

        public string PrivacyUrl { get; }

        public string Rating { get; }

        public ImageInfo AdIcon { get; }

        public ImageInfo IconImage { get; }

        public IReadOnlyList<ImageInfo> Images { get; }

        public IReadOnlyDictionary<string, string> AdditionalAssets { get; }

        public void Dispose()
        {
            Context.Dispose();
        }

        public void RegisterContainer(FrameworkElement container)
        {
            Context.RegisterAdContainer(container);
        }

        public void RegisterContainer(FrameworkElement container, IList<FrameworkElement> clickableElements)
        {
            Context.RegisterAdContainer(container, clickableElements);
        }
    }
}
