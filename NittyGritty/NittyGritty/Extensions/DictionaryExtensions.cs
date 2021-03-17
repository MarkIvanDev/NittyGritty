using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Extensions
{
    public static class DictionaryExtensions
    {

        public static TValue Get<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue = default)
        {
            return dictionary.TryGetValue(key, out var value) ?
                value : defaultValue;
        }

        public static TValue Extract<TKey, TValue>(this IDictionary<TKey, object> dictionary, TKey key, TValue defaultValue = default)
        {
            return dictionary.TryGetValue(key, out var v) && v is TValue value ?
                value : defaultValue;
        }

        public static TValue Extract<TValue>(this IDictionary<string, object> dictionary, string key, TValue defaultValue = default)
        {
            return Extract<string, TValue>(dictionary, key, defaultValue);
        }

    }
}
