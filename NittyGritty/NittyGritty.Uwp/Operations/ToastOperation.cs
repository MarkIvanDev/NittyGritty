using NittyGritty.Models;
using NittyGritty.Views.Payloads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp.Operations
{
    public class ToastOperation
    {
        private readonly Dictionary<string, KeyViewConfiguration> configurations;

        public ToastOperation(string key)
        {
            if(string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("Key cannot be null, empty, or whitespace", nameof(key));
            }

            Key = key;
            configurations = new Dictionary<string, KeyViewConfiguration>();
        }

        public string Key { get; }

        /// <summary>
        /// Configures the values that this key can handle with the appropriate view
        /// </summary>
        /// <param name="value">The value that this key can handle. A value can be configured with an empty string
        /// A value with a value of * will be used as fallback for unknown values</param>
        /// <param name="view">The type of the view that the value leads to</param>
        public void Configure(string value, Type view, Predicate<QueryString> createsNewView = null)
        {
            lock (configurations)
            {
                if (value.Trim().Length == 0 && !value.Equals(string.Empty))
                {
                    throw new ArgumentException("Value cannot consist of whitespace only");
                }

                if (configurations.ContainsKey(value))
                {
                    throw new ArgumentException("This value is already used: " + value);
                }

                if (view == null)
                {
                    throw new ArgumentNullException(nameof(view), "View cannot be null");
                }

                var configuration = new KeyViewConfiguration(value, view, createsNewView);

                configurations.Add(
                    value,
                    configuration);
            }
        }

        public virtual async Task Run(ToastNotificationActivatedEventArgs args, Frame frame)
        {
            var query = QueryString.Parse(args.Argument);
            if(!query.Contains(Key))
            {
                return;
            }

            var value = query[Key];
            var payload = new ToastPayload(args.UserInput, query);

            KeyViewConfiguration configuration = null;
            lock (configurations)
            {
                if (configurations.TryGetValue(value, out var view))
                {
                    configuration = view;
                }
                else
                {
                    if (configurations.TryGetValue("*", out var fallbackView))
                    {
                        configuration = fallbackView;
                    }
                    else
                    {
                        throw new ArgumentException(
                            string.Format(
                                "No configuration for value: {0}. Did you forget to call ToastOperation.Configure?",
                                value),
                            nameof(value));
                    }
                }
            }

            await configuration.Run(query, payload, args.CurrentlyShownApplicationViewId, frame);
        }
    }
}
