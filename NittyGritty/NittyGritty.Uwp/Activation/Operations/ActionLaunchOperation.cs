using NittyGritty.Models;
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
    public class ActionLaunchOperation : LaunchOperation
    {
        private readonly Dictionary<string, ActionLaunchConfiguration> configurations;

        public ActionLaunchOperation(LaunchSource source, string actionKey, params ActionLaunchConfiguration[] configurations) : base(source)
        {
            if(source != LaunchSource.Secondary && source != LaunchSource.Jumplist)
            {
                throw new ArgumentException("Action Launch Operation can only be from a Secondary or JumpList launch source.");
            }

            if (string.IsNullOrWhiteSpace(actionKey))
            {
                throw new ArgumentException("Action Key cannot be null, empty, or whitespace", nameof(actionKey));
            }

            ActionKey = actionKey;
            this.configurations = new Dictionary<string, ActionLaunchConfiguration>();
            foreach (var config in configurations)
            {
                this.configurations.Add(config.Action, config);
            }
        }

        public string ActionKey { get; }

        public override async Task Run(LaunchActivatedEventArgs args, Frame frame)
        {
            var query = QueryString.Parse(args.Arguments);
            if (!query.Contains(ActionKey))
            {
                return;
            }

            var action = query[ActionKey];
            var configuration = GetConfiguration(action);
            await configuration.View.Show(query, args.CurrentlyShownApplicationViewId, frame);
        }

        private ActionLaunchConfiguration GetConfiguration(string action)
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
                            $"No configuration for action: {action}. Did you forget to register an ActionLaunchConfiguration?",
                            nameof(action));
                    }
                }
            }
        }
    }
}
