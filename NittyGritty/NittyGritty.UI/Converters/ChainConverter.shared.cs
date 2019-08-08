using System;
using System.Collections.ObjectModel;
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
    public class ChainConverter : Collection<IValueConverter>, IValueConverter
    {
#if UWP
        public object Convert(object value, Type targetType, object parameter, string language)
#else
        public object Convert(object value, Type targetType, object parameter, CultureInfo language)
#endif
        {
            return this.Aggregate(value, (current, converter) =>
#if UWP
                current == DependencyProperty.UnsetValue ?
#else
                current == null ?
#endif
                    current :
                    converter.Convert(current, targetType, parameter, language)
            );
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
