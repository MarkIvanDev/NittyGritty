using NittyGritty.Models;
using NittyGritty.Platform.Payloads;
using NittyGritty.Uwp.Activation.Operations.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp.Activation.Operations
{
    public class ToastOperation : IViewOperation<ToastNotificationActivatedEventArgs>
    {
        private readonly Dictionary<string, ToastConfiguration> configurations;

        public ToastOperation(string actionKey, params ToastConfiguration[] configurations)
        {
            if(string.IsNullOrWhiteSpace(actionKey))
            {
                throw new ArgumentException("Action Key cannot be null, empty, or whitespace", nameof(actionKey));
            }

            ActionKey = actionKey;
            this.configurations = new Dictionary<string, ToastConfiguration>();
            foreach (var config in configurations)
            {
                this.configurations.Add(config.Action, config);
            }
        }

        public string ActionKey { get; }

        public async Task Run(ToastNotificationActivatedEventArgs args, Frame frame)
        {
            var query = QueryString.Parse(args.Argument);
            if(!query.Contains(ActionKey))
            {
                return;
            }
            
            var action = query[ActionKey];
            var payload = new ToastPayload(args.UserInput, query);

            var configuration = GetConfiguration(action);
            await configuration.View.Show(payload, args.CurrentlyShownApplicationViewId, frame);
        }

        private ToastConfiguration GetConfiguration(string action)
        {
            lock (configurations)
            {
                if (configurations.TryGetValue(action, out var config))
                {
                    return config;
                }
                else
                {
                    if (configurations.TryGetValue("*", out var fallbackConfig))
                    {
                        return fallbackConfig;
                    }
                    else
                    {
                        throw new ArgumentException(
                            $"No configuration for action: {action}. Did you forget to register a ToastConfiguration?",
                            nameof(action));
                    }
                }
            }
        }
    }
}
