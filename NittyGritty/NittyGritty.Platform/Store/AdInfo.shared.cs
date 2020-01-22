using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Platform.Store
{
    public class AdInfo : ObservableObject
    {
        public AdInfo(string id, AdType type, string appId)
        {
            Id = id;
            Type = type;
            AppId = appId;
        }

        public string Id { get; }

        public AdType Type { get; }

        public string AppId { get; }

        public enum AdType
        {
            Unknown = 0,
            Banner = 1,
            InterstitialBanner = 2,
            InterstitialVideo = 3,
            Native = 4
        }
    }
}
