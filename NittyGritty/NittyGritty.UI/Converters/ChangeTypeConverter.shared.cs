using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
#if UWP
using Windows.UI.Xaml.Data;
#else
using System.Globalization;
using Xamarin.Forms;
#endif

namespace NittyGritty.UI.Converters
{
    public class ChangeTypeConverter : IValueConverter
    {
#if UWP
        public object Convert(object value, Type targetType, object parameter, string language)
#else
        public object Convert(object value, Type targetType, object parameter, CultureInfo language)
#endif
        {
            if (targetType.IsConstructedGenericType && targetType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return null;
                }
                targetType = Nullable.GetUnderlyingType(targetType);
            }

            if (value == null && targetType.GetTypeInfo().IsValueType)
                return Activator.CreateInstance(targetType);

            if (targetType.IsInstanceOfType(value))
                return value;

            return System.Convert.ChangeType(value, targetType);
        }

#if UWP
        public object ConvertBack(object value, Type targetType, object parameter, string language)
#else
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language)
#endif
        {
            return Convert(value, targetType, parameter, language);
        }
    }
}
