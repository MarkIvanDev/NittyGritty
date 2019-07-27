using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.Uwp.Operations
{
    public abstract class KeyViewOperation<T>
    {
        private readonly Dictionary<string, KeyViewConfiguration<T>> configurations;

        public KeyViewOperation()
        {
            configurations = new Dictionary<string, KeyViewConfiguration<T>>();
        }

        public virtual void Configure(string key, Type view, Predicate<T> createsNewView = null)
        {
            lock (configurations)
            {
                if (CheckConfiguration(key, view))
                {
                    var configuration = new KeyViewConfiguration<T>(key, view, createsNewView);

                    configurations.Add(
                        key,
                        configuration);
                }
            }
        }

        private bool CheckConfiguration(string key, Type value)
        {
            if (key.Trim().Length == 0 && !key.Equals(string.Empty))
            {
                throw new ArgumentException("Key cannot consist of whitespace only");
            }

            if (configurations.ContainsKey(key))
            {
                throw new ArgumentException("This key is already used: " + key);
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value), "View cannot be null");
            }
            return true;
        }

        internal KeyViewConfiguration<T> GetConfiguration(string key)
        {
            lock (configurations)
            {
                if (configurations.TryGetValue(key, out var view))
                {
                    return view;
                }
                else
                {
                    if (configurations.TryGetValue("*", out var fallbackView))
                    {
                        return fallbackView;
                    }
                    else
                    {
                        throw new ArgumentException(
                            string.Format(
                                "No configuration for key: {0}. Did you forget to call Operation.Configure?",
                                key),
                            nameof(key));
                    }
                }
            }
        }
    }
}
