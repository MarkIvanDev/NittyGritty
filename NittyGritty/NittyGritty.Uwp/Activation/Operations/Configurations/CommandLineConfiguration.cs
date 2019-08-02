using NittyGritty.Uwp.Platform;
using NittyGritty.Uwp.Platform.Payloads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.Uwp.Activation.Operations.Configurations
{
    public class CommandLineConfiguration
    {
        public CommandLineConfiguration(string command, MultiViewConfiguration<CommandLinePayload> view)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            if (command.Trim().Length == 0 && !command.Equals(string.Empty))
            {
                throw new ArgumentException("Path cannot consist of whitespace only");
            }

            Command = command;
            View = view ?? throw new ArgumentNullException(nameof(view));
        }

        public string Command { get; }

        public MultiViewConfiguration<CommandLinePayload> View { get; }
    }
}
