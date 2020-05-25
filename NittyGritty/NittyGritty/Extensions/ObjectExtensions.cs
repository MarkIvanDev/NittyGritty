using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    }
}
