using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Extensions;
using Windows.UI.Xaml.Data;

namespace NittyGritty.Uno.Converters
{
    public class ValueWhenConverter : IValueConverter
    {

        public object When { get; set; }

        public object Value { get; set; }

        public object Otherwise { get; set; }

        public object OtherwiseValueBack { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
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

        public object ConvertBack(object value, Type targetType, object parameter, string language)
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
    }
}
