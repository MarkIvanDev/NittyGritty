using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.Extensions
{
    public static class ObjectExtensions
    {

        public static bool IsNull(this object value)
        {
            return value == null;
        }

        public static bool IsNotNull(this object value)
        {
            return value != null;
        }

        public static bool Is<T>(this T value, Func<T, bool> func)
        {
            return func(value);
        }

        public static async Task<bool> Is<T>(this T value, Func<T, Task<bool>> func)
        {
            return await func(value);
        }

        public static bool Is<T>(this object value)
        {
            if (value.IsNull())
            {
                return false;
            }

            var converter = TypeDescriptor.GetConverter(typeof(T));
            return converter?.CanConvertTo(typeof(T)) ?? false;
        }

        public static T To<T>(this object value, T defaultValue = default(T))
        {
            if (value.IsNull())
            {
                return defaultValue;
            }

            var converter = TypeDescriptor.GetConverter(typeof(T));
            if (converter != null)
            {
                try
                {
                    return (T)converter.ConvertFromString(value.ToString());
                }
                catch (Exception)
                {
                    return defaultValue;
                }

            }
            return defaultValue;
        }

        public static bool IsDefault<T>(this T value)
        {
            return value.Equals(default(T));
        }

        public static bool EqualTo(this object value1, object value2)
        {
            try
            {
                if (value1 == value2)
                {
                    return true;
                }

                if (value1 != null && value2 != null)
                {
                    var v2 = value1 is Enum ?
                        Enum.IsDefined(value1.GetType(), value2) ? Enum.ToObject(value1.GetType(), value2) : null :
                        Convert.ChangeType(value2, value1.GetType(), CultureInfo.InvariantCulture);
                    if (value1.Equals(v2))
                    {
                        return true;
                    }

                    var v1 = value2 is Enum ?
                        Enum.IsDefined(value2.GetType(), value1) ? Enum.ToObject(value2.GetType(), value1) : null :
                        Convert.ChangeType(value1, value2.GetType(), CultureInfo.InvariantCulture);
                    if (value2.Equals(v1))
                    {
                        return true;
                    }

                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
