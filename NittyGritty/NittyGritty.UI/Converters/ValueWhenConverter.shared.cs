using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Extensions;
#if UWP
using Windows.UI.Xaml.Data;
#else
using System.Globalization;
using Xamarin.Forms;
#endif

namespace NittyGritty.UI.Converters
{
    public class ValueWhenConverter : IValueConverter
    {
#if UWP
        public object Convert(object value, Type targetType, object parameter, string language)
#else
        public object Convert(object value, Type targetType, object parameter, CultureInfo language)
#endif
        {
            try
            {
                if (value.EqualTo(parameter ?? When))
                {
                    return Value;
                }

                return Otherwise;
            }
            catch
            {
                return Otherwise;
            }
        }

#if UWP
        public object ConvertBack(object value, Type targetType, object parameter, string language)
#else
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language)
#endif
        {
            if (OtherwiseValueBack == null)
                throw new InvalidOperationException("Cannot ConvertBack if no OtherwiseValueBack is set!");

            try
            {
                if (value.EqualTo(Value))
                {
                    return When;
                }

                return OtherwiseValueBack;
            }
            catch
            {
                return OtherwiseValueBack;
            }
        }

        public object Value { get; set; }

        public object Otherwise { get; set; }

        public object When { get; set; }

        public object OtherwiseValueBack { get; set; }
    }
}
