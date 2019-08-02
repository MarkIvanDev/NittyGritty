using NittyGritty.Utilities;
using NittyGritty.Uwp.Activation.Operations.Configurations;
using NittyGritty.Uwp.Platform.Payloads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp.Activation.Operations
{
    public class CommandLineOperation : IViewOperation<CommandLineActivatedEventArgs>
    {
        private readonly Dictionary<string, CommandLineConfiguration> configurations;

        public CommandLineOperation(params CommandLineConfiguration[] configurations)
        {
            this.configurations = new Dictionary<string, CommandLineConfiguration>();
            foreach (var config in configurations)
            {
                this.configurations.Add(config.Command, config);
            }
        }

        public virtual async Task Run(CommandLineActivatedEventArgs args, Frame frame)
        {
            var deferral = args.Operation.GetDeferral();

            var folder = await StorageFolder.GetFolderFromPathAsync(args.Operation.CurrentDirectoryPath);
            var payload = new CommandLinePayload(args.Operation, folder);

            var configuration = GetConfiguration(payload.Command);
            int currentApplicationViewId = ApplicationView.GetApplicationViewIdForWindow(CoreApplication.GetCurrentView().CoreWindow);
            await configuration.View.Show(payload, currentApplicationViewId, frame);

            deferral.Complete();
        }

        private CommandLineConfiguration GetConfiguration(string command)
        {
            lock (configurations)
            {
                if (configurations.TryGetValue(command, out var config))
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
                            $"No configuration for command: {command}. Did you forget to register a CommandLineConfiguration?",
                            nameof(command));
                    }
                }
            }
        }
    }
}
