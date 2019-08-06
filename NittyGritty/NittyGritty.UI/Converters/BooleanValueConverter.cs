using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace NittyGritty.UI.Converters
{
    public class BooleanValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (bool.TryParse(value?.ToString(), out var result))
            {
                return result ? TrueValue : FalseValue;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        public object TrueValue { get; set; }

        public object FalseValue { get; set; }

    }
}
