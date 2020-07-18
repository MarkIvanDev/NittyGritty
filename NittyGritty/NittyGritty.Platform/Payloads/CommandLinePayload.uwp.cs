using System;
using System.Text;
using NittyGritty.Models;
using NittyGritty.Platform.Storage;
using NittyGritty.Utilities;
using Windows.ApplicationModel.Activation;
using Windows.Storage;

namespace NittyGritty.Platform.Payloads
{
    public class CommandLinePayload : ICommandLinePayload
    {
        public CommandLinePayload(CommandLineActivationOperation operation, IStorageFolder folder)
        {
            Operation = operation;
            OriginalArguments = operation.Arguments;
            CurrentDirectory = new NGFolder(folder);
            var parsedCommand = CommandLineUtilities.Parse(operation.Arguments);
            Command = parsedCommand.Command;
            Parameters = parsedCommand.Parameters;
        }

        public CommandLineActivationOperation Operation { get; }

        public string OriginalArguments { get; }

        public IFolder CurrentDirectory { get; }

        public string Command { get; }

        public QueryString Parameters { get; }
    }
}
