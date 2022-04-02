using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace NittyGritty.Uno.Converters
{
    public class IsLessThanConverter : IValueConverter
    {
        public object Value { get; set; } = 0;

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            try
            {
                return value is IComparable comparable &&
                    comparable.CompareTo(System.Convert.ChangeType(Value, comparable.GetType())) < 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
