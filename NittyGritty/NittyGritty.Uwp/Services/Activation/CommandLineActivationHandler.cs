using NittyGritty.Models;
using NittyGritty.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;

namespace NittyGritty.Uwp.Services.Activation
{
    public class CommandLineActivationHandler : ActivationHandler<CommandLineActivatedEventArgs>
    {
        public CommandLineActivationHandler()
        {

        }

        public Func<ReadOnlyCollection<ParsedCommand>, string, Task<int>> ExecuteCommands { get; set; }

        public override async Task HandleAsync(CommandLineActivatedEventArgs args)
        {
            var deferral = args.Operation.GetDeferral();
            var commands = CommandLineUtilities.Parse(args.Operation.Arguments);
            args.Operation.ExitCode = await ExecuteCommands?.Invoke(commands, args.Operation.CurrentDirectoryPath);
            deferral.Complete();
        }
    }
}
