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
            names = enums.ToDictionary(i => i.ToString(), i => i, StringComparer.Ordinal);
            var nonZeroEnums = new List<TEnum>(enums);
            nonZeroEnums.Remove((TEnum)(object)0);

            UnderlyingType = Enum.GetUnderlyingType(typeof(TEnum));
            Values = new ReadOnlyCollection<TEnum>(enums);
            NonZeroValues = new ReadOnlyCollection<TEnum>(nonZeroEnums);
            Names = new ReadOnlyCollection<string>(names.Keys.ToList());
        }

        public static Type UnderlyingType { get; }

        public static IReadOnlyList<TEnum> Values { get; }

        public static IReadOnlyList<TEnum> NonZeroValues { get; }

        public static IReadOnlyList<string> Names { get; }

        public static bool IsDefined(TEnum value)
        {
            return values.Contains(value);
        }

        public static bool IsDefined(string name, bool ignoreCase = false)
        {
            return !ignoreCase ? names.ContainsKey(name) : names.Keys.Any(k => k.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public static TEnum ParseOrDefault(string value, TEnum defaultValue = default, bool ignoreCase = false)
        {
            if (!ignoreCase)
            {
                return names.TryGetValue(value, out var item) ? item : defaultValue;
            }
            else
            {
                foreach (var item in names)
                {
                    if (item.Key.Equals(value, StringComparison.OrdinalIgnoreCase))
                    {
                        return item.Value;
                    }
                }
                return defaultValue;
            }
        }

        public static bool TryParse(string value, out TEnum result, bool ignoreCase = false)
        {
            if (!ignoreCase)
            {
                return names.TryGetValue(value, out result);
            }
            else
            {
                foreach (var item in names)
                {
                    if (item.Key.Equals(value, StringComparison.OrdinalIgnoreCase))
                    {
                        result = item.Value;
                        return true;
                    }
                }
                result = default;
                return false;
            }
        }

    }
}
