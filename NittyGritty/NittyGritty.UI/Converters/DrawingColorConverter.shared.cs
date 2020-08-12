using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if UWP
using Windows.UI;
using Windows.UI.Xaml.Data;
#else
using System.Globalization;
using Xamarin.Forms;
#endif

namespace NittyGritty.UI.Converters
{
    public class DrawingColorConverter : IValueConverter
    {
#if UWP
        public object Convert(object value, Type targetType, object parameter, string language)
#else
        public object Convert(object value, Type targetType, object parameter, CultureInfo language)
#endif
        {
            if (value is System.Drawing.Color colorInfo)
            {
#if UWP
                return Color.FromArgb(colorInfo.A, colorInfo.R, colorInfo.G, colorInfo.B);
#else
                return Color.FromRgba(colorInfo.R, colorInfo.G, colorInfo.B, colorInfo.A);
#endif
            }
            return null;
        }

#if UWP
        public object ConvertBack(object value, Type targetType, object parameter, string language)
#else
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language)
#endif
        {
            if (value is Color color)
            {
                return System.Drawing.Color.FromArgb((byte)color.A, (byte)color.R, (byte)color.G, (byte)color.B);
            }
            return null;
        }
    }
}
