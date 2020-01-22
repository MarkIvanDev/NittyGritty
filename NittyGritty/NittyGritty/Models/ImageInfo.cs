using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Models
{
    public class ImageInfo
    {
        public ImageInfo(Uri url, double height, double width)
        {
            Url = url;
            Height = height;
            Width = width;
        }

        public Uri Url { get; }

        public double Height { get; }

        public double Width { get; }
    }
}
