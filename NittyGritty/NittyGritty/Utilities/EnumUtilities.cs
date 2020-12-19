using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Utilities
{
    public static class EnumUtilities<TEnum> where TEnum : struct, Enum
    {
        private static readonly HashSet<TEnum> valueSet;

        static EnumUtilities()
        {
            Values = (TEnum[])Enum.GetValues(typeof(TEnum));
            valueSet = new HashSet<TEnum>(Values);
        }

        public static TEnum[] Values { get; }

        public static bool IsDefined(TEnum value)
        {
            return valueSet.Contains(value);
        }
    }
}
