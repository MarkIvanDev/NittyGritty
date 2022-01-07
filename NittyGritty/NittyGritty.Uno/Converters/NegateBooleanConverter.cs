using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace NittyGritty.Uno.Converters
{
    public class NegateBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return bool.TryParse(value?.ToString(), out bool result) ?
                !result : (bool?)null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return bool.TryParse(value?.ToString(), out bool result) ?
                !result : (bool?)null;
        }
    }
}
