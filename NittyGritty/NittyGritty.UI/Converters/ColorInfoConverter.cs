using NittyGritty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;

namespace NittyGritty.UI.Converters
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
            if(value is Color color)
            {
                return new ColorInfo(color.A, color.R, color.G, color.B);
            }
            return null;
        }
    }
}
