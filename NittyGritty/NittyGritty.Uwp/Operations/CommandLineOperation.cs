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
    public class CommandLineOperation : NormalOperation<CommandLinePayload>
    {
        public CommandLineOperation(string alias)
        {
            if (string.IsNullOrWhiteSpace(alias))
            {
                throw new ArgumentException("Alias cannot be null, empty, or whitespace.", nameof(alias));
            }

            Alias = alias;
        }

        public string Alias { get; }

        public virtual async Task Run(CommandLineActivatedEventArgs args, Frame frame)
        {
            var deferral = args.Operation.GetDeferral();

            var currentDirectory = args.Operation.CurrentDirectoryPath;
            var (command, parameters) = CommandLineUtilities.Parse(args.Operation.Arguments);
            var payload = new CommandLinePayload(command, currentDirectory, parameters);

            var configuration = GetConfiguration(command);
            int currentApplicationViewId = ApplicationView.GetApplicationViewIdForWindow(CoreApplication.GetCurrentView().CoreWindow);
            await configuration.Show(payload, currentApplicationViewId, frame);

            deferral.Complete();
        }
    }
}
