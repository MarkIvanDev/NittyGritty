using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if UWP
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
#else
using System.Globalization;
using Xamarin.Forms;
#endif

namespace NittyGritty.UI.Converters
{
    public class StringVisibilityConverter : IValueConverter
    {
#if UWP
        public object Convert(object value, Type targetType, object parameter, string language)
#else
        public object Convert(object value, Type targetType, object parameter, CultureInfo language)
#endif
        {
            if (string.IsNullOrEmpty(value?.ToString()))
            {
#if UWP
                return Visibility.Collapsed;
#else
                return false;
#endif          
            }
            else
            {
#if UWP
                return Visibility.Visible;
#else
                return true;
#endif
            }
        }

#if UWP
        public object ConvertBack(object value, Type targetType, object parameter, string language)
#else
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language)
#endif
        {
            throw new NotImplementedException();
        }
    }
}
