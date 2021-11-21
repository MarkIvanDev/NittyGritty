using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NittyGritty.Services.Core;

namespace NittyGritty.Services
{
    public partial class ResourceService<T> : IResourceService<T>
    {
        private readonly Dictionary<string, T> resources = new Dictionary<string, T>();

        public void Configure(string key, T value)
        {
            lock (resources)
            {
                if (resources.ContainsKey(key))
                {
                    throw new ArgumentException("This key is already used: " + key);
                }

                var pair = resources.FirstOrDefault(p => EqualityComparer<T>.Default.Equals(p.Value, value));
                if (pair.Key != default)
                {
                    throw new ArgumentException(
                        "This value is already configured with key " + pair.Key);
                }

                resources.Add(
                    key,
                    value);
            }
        }

        public string GetKeyForValue(T value)
        {
            lock (resources)
            {
                if (resources.ContainsValue(value))
                {
                    return resources.FirstOrDefault(p => EqualityComparer<T>.Default.Equals(p.Value, value)).Key;
                }
                else
                {
                    throw new ArgumentException($"The value '{value}' is unknown by the ResourceService");
                }
            }
        }

        public T GetValue(string key)
        {
            return resources.TryGetValue(key, out var value) ? value : default;
        }
    }
}
