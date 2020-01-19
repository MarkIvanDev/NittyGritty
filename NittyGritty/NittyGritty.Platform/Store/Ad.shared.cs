using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Platform.Store
{
    public abstract class Ad : ObservableObject
    {
        public Ad(string id, AdType type)
        {
            Id = id;
            Type = type;
        }

        public string Id { get; }

        public AdType Type { get; }

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
