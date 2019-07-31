using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.Uwp.Services.Activation
{
    public abstract class HostedActivationHandler<T> : ActivationHandler<T>
        where T : class
    {
        private readonly Dictionary<string, SingleViewConfiguration<T>> configurations;

        public HostedActivationHandler(ActivationStrategy strategy) : base(strategy)
        {
            configurations = new Dictionary<string, SingleViewConfiguration<T>>();
        }

        protected virtual void Configure(string key, Type view)
        {
            lock (configurations)
            {
                if (CheckConfiguration(key, view))
                {
                    var configuration = new SingleViewConfiguration<T>(key, view);
                    configurations.Add(key, configuration);
                }
            }
        }

        protected virtual bool CheckConfiguration(string key, Type value)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key), "Key cannot be null");
            }

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

        protected SingleViewConfiguration<T> GetConfiguration(string key)
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
