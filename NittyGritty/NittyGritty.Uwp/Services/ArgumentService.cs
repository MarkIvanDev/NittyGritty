using NittyGritty.Models;
using NittyGritty.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp.Services
{
    public class ArgumentService : IConfigurable<Type>
    {
        private readonly Dictionary<string, Type> configuration = new Dictionary<string, Type>();

        public void Configure(string key, Type value)
        {
            lock (configuration)
            {
                if (!configuration.TryAdd(key, value))
                {
                    throw new ArgumentException("This key is already used: " + key);
                }
            }
        }

        public string GetKeyForValue(Type value)
        {
            lock (configuration)
            {
                var config = configuration.FirstOrDefault(p => p.Value == value);
                if (config.Key is null)
                {
                    throw new ArgumentException($"The value '{value.Name}' is unknown by the {nameof(ArgumentService)}");
                }
                else
                {
                    return config.Key;
                }
            }
        }

        public Type GetValue(string key)
        {
            lock (configuration)
            {
                if (configuration.TryGetValue(key, out var value))
                {
                    return value;
                }
                else
                {
                    throw new ArgumentException(
                       $"No such key: {key}. Did you forget to call ArgumentService.Configure?",
                       nameof(key));
                }
            }
        }

        public Type GetValue(string arguments, string actionKey)
        {
            var query = QueryString.Parse(arguments);
            return query.TryGetValue(actionKey, out var key) ?
                GetValue(key) :
                null;
        }
    }
}
