using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NittyGritty.Services.Core
{
    public interface IConfigurable<T>
    {
        void Configure(string key, T value);

        string GetKeyForValue(T value);

        T GetValue(string key);

        IReadOnlyCollection<string> Keys { get; }

        IReadOnlyCollection<T> Values { get; }
    }

    public class ConfigurableService<T> : IConfigurable<T>
    {
        private readonly object _lock = new object();
        private readonly Dictionary<string, T> configurations = new Dictionary<string, T>();

        public void Configure(string key, T value)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentException($"'{nameof(key)}' cannot be null or whitespace.", nameof(key));
            if (value == null) throw new ArgumentNullException(nameof(value));

            lock (_lock)
            {
                if (configurations.ContainsKey(key))
                {
                    throw new ArgumentException($"This key is already used: {key}");
                }

                if (configurations.ContainsValue(value))
                {
                    throw new ArgumentException($"This value is already used: {value}");
                }

                configurations.Add(key, value);
            }
        }

        public string GetKeyForValue(T value)
        {
            lock (_lock)
            {
                return configurations.FirstOrDefault(i => i.Value.Equals(value)).Key ??
                    throw new ArgumentException($"The value '{value}' is unknown by the Service");
            }
        }

        public T GetValue(string key)
        {
            lock (_lock)
            {
                return configurations.TryGetValue(key, out T value) ? value :
                    throw new ArgumentException($"The key '{key}' is unknown by the Service");
            }
        }

        public IReadOnlyCollection<string> Keys => configurations.Keys;

        public IReadOnlyCollection<T> Values => configurations.Values;
    }
}
