using NittyGritty.Models;
using NittyGritty.Utilities;
using NittyGritty.Views.Payloads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp.Operations
{
    public class CommandLineOperation
    {
        private readonly Dictionary<string, KeyViewConfiguration> configurations;

        public CommandLineOperation(string alias)
        {
            configurations = new Dictionary<string, KeyViewConfiguration>();

            if (string.IsNullOrWhiteSpace(alias))
            {
                throw new ArgumentException("Alias cannot be null, empty, or whitespace.", nameof(alias));
            }

            Alias = alias;
        }

        public string Alias { get; }

        /// <summary>
        /// Configures the commands that this alias can handle with the appropriate view
        /// </summary>
        /// <param name="command">The command that this alias can handle. A command can be configured with an empty string
        /// A command with a value of * will be used as fallback for unknown commands</param>
        /// <param name="view">The type of the view that the path leads to</param>
        public void Configure(string command, Type view, Predicate<QueryString> createsNewView = null)
        {
            lock (configurations)
            {
                if (command.Trim().Length == 0 && !command.Equals(string.Empty))
                {
                    throw new ArgumentException("Command cannot consist of whitespace only");
                }

                if (configurations.ContainsKey(command))
                {
                    throw new ArgumentException("This command is already used: " + command);
                }

                if (view == null)
                {
                    throw new ArgumentNullException(nameof(view), "View cannot be null");
                }

                var configuration = new KeyViewConfiguration(command, view, createsNewView);

                configurations.Add(
                    command,
                    configuration);
            }
        }

        public virtual async Task Run(CommandLineActivatedEventArgs args, Frame frame)
        {
            var deferral = args.Operation.GetDeferral();

            var currentDirectory = args.Operation.CurrentDirectoryPath;
            var (command, parameters) = CommandLineUtilities.Parse(args.Operation.Arguments);
            var payload = new CommandLinePayload(command, currentDirectory, parameters);

            KeyViewConfiguration configuration = null;
            lock (configurations)
            {
                if (configurations.TryGetValue(command, out var view))
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
                                "No such command: {0}. Did you forget to call Configure?",
                                command),
                            nameof(command));
                    }
                }
            }
            int currentApplicationViewId = ApplicationView.GetApplicationViewIdForWindow(CoreApplication.GetCurrentView().CoreWindow);
            await configuration.Run(parameters, payload, currentApplicationViewId, frame);

            deferral.Complete();
        }
    }
}
