using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace NittyGritty.Extensions
{
    public static class ColorExtensions
    {
        public static string ToHex(this Color color)
        {
            return $"#{color.A:X2}{color.R:X2}{color.G:X2}{color.B:X2}";
        }
    }
}
