using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace NittyGritty.Utilities
{
    public static class EnumHelper<TEnum> where TEnum : struct, Enum
    {
        private static readonly HashSet<TEnum> values;
        private static readonly Dictionary<string, TEnum> names;

        static EnumHelper()
        {
            var enums = (TEnum[])Enum.GetValues(typeof(TEnum));
            values = new HashSet<TEnum>(enums);
            names = enums.ToDictionary(i => i.ToString(), i => i, StringComparer.OrdinalIgnoreCase);
            UnderlyingType = Enum.GetUnderlyingType(typeof(TEnum));
            Values = new ReadOnlyCollection<TEnum>(enums);
            Names = new ReadOnlyCollection<string>(names.Keys.ToList());
        }

        public static Type UnderlyingType { get; }

        public static IReadOnlyList<TEnum> Values { get; }

        public static IReadOnlyList<string> Names { get; }

        public static bool IsDefined(TEnum value)
        {
            return values.Contains(value);
        }

        public static bool IsDefined(string name)
        {
            return names.ContainsKey(name);
        }

        public static TEnum ParseOrDefault(string value, TEnum defaultValue = default)
        {
            return names.TryGetValue(value, out var item) ? item : defaultValue;
        }

        public static bool TryParse(string value, out TEnum result)
        {
            return names.TryGetValue(value, out result);
        }

    }
}
