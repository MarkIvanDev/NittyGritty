using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace NittyGritty.Uno.Converters
{
    public class DrawingColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is System.Drawing.Color colorInfo)
            {
                return Windows.UI.Color.FromArgb(colorInfo.A, colorInfo.R, colorInfo.G, colorInfo.B);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is Windows.UI.Color color)
            {
                return System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
            }
            return null;
        }
    }
}
