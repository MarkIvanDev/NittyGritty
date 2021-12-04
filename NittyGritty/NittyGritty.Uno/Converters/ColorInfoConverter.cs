using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Models;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace NittyGritty.Uno.Converters
{
    public class ColorInfoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is ColorInfo colorInfo)
            {
                return Color.FromArgb(colorInfo.A, colorInfo.R, colorInfo.G, colorInfo.B);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is Color color)
            {
                return new ColorInfo((byte)color.A, (byte)color.R, (byte)color.G, (byte)color.B);
            }
            return null;
        }
    }
}
