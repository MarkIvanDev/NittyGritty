using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Extensions
{
    public static class ComparableExtensions
    {

        public static bool IsGreaterThan<T>(this T value, T other) where T : IComparable<T>
        {
            return value.CompareTo(other) > 0;
        }

        public static bool IsGreaterThanOrEqualTo<T>(this T value, T other) where T : IComparable<T>
        {
            return value.CompareTo(other) >= 0;
        }

        public static bool IsLesserThan<T>(this T value, T other) where T : IComparable<T>
        {
            return value.CompareTo(other) < 0;
        }

        public static bool IsLesserThanOrEqualTo<T>(this T value, T other) where T : IComparable<T>
        {
            return value.CompareTo(other) <= 0;
        }

        public static bool IsEqualTo<T>(this T value, T other) where T : IComparable<T>
        {
            return value.CompareTo(other) == 0;
        }

        public static bool IsNotEqualTo<T>(this T value, T other) where T : IComparable<T>
        {
            return value.CompareTo(other) != 0;
        }

        public static bool IsBetween<T>(this T value, T from, T to, bool isExclusive = true) where T : IComparable<T>
        {
            var result = false;
            if (isExclusive)
            {
                result = value.CompareTo(from) > 0 && value.CompareTo(to) < 0;
            }
            else
            {
                result = value.CompareTo(from) >= 0 && value.CompareTo(to) <= 0;
            }
            return result;
        }

    }
}
