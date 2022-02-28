using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NittyGritty.Utilities
{
    public static class AttributeUtilities
    {
        public static TAttribute GetMemberAttribute<T, TAttribute>(string member)
            where TAttribute : Attribute
        {
            return typeof(T)
                .GetMember(member)
                .FirstOrDefault()?
                .GetCustomAttributes<TAttribute>()?
                .FirstOrDefault();
        }

        public static IList<TAttribute> GetMemberAttributes<T, TAttribute>(string member)
            where TAttribute : Attribute
        {
            return typeof(T)
                .GetMember(member)
                .FirstOrDefault()?
                .GetCustomAttributes<TAttribute>()?
                .ToList() ?? new List<TAttribute>();
        }

        public static TAttribute GetEnumAttribute<TEnum, TAttribute>(this TEnum @enum)
            where TEnum : struct, Enum
            where TAttribute : Attribute
        {
            return GetMemberAttribute<TEnum, TAttribute>(@enum.ToString());
        }

        public static IList<TAttribute> GetEnumAttributes<TEnum, TAttribute>(this TEnum @enum)
            where TEnum : struct, Enum
            where TAttribute : Attribute
        {
            return GetMemberAttributes<TEnum, TAttribute>(@enum.ToString());
        }

    }
}
