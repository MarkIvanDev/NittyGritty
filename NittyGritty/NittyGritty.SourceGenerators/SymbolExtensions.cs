using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;

namespace NittyGritty.SourceGenerators
{
    internal static class SymbolExtensions
    {
		public static AttributeData GetAttribute<T>(this ISymbol symbol)
        {
            return symbol.GetAttributes().FirstOrDefault(a => a.AttributeClass?.ToDisplayString() == typeof(T).FullName);
        }

        public static IList<AttributeData> GetAttributes<T>(this ISymbol symbol)
        {
            return symbol.GetAttributes().Where(a => a.AttributeClass?.ToDisplayString() == typeof(T).FullName).ToList();
        }

        public static T GetValue<T>(this AttributeData attribute, string key)
        {
            var value = attribute.NamedArguments.FirstOrDefault(a => a.Key == key).Value;
            return value.IsNull ? default(T) : (T)value.Value;
        }

        public static bool IsDerivedFromType(this INamedTypeSymbol symbol, string type)
        {
            if (symbol.ToDisplayString() == type)
            {
                return true;
            }

            if (symbol.BaseType == null)
            {
                return false;
            }

            return symbol.BaseType.IsDerivedFromType(type);
        }
	}
}
