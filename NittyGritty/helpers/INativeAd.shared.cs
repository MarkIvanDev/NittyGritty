using System;
using System.Collections.Generic;
using System.Text;
using NittyGritty.Models;
using NittyGritty.Platform.Events;

namespace NittyGritty.Platform.Store
{
    public interface INativeAd : IDisposable
    {
        string Title { get; }

        string Description { get; }

        string CallToActionText { get; }

        string SponsoredBy { get; }

        string Price { get; }

        string PrivacyUrl { get; }

        string Rating { get; }

        ImageInfo AdIcon { get; }

        ImageInfo IconImage { get; }

        IReadOnlyList<ImageInfo> Images { get; }

        IReadOnlyDictionary<string, string> AdditionalAssets { get; }

        event EventHandler<NativeAdClickedEventArgs> AdClicked;
    }
}
