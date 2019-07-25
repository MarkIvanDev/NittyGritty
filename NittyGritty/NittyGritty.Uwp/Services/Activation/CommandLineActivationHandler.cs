using NittyGritty.Models;
using NittyGritty.Utilities;
using NittyGritty.Uwp.Operations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;

namespace NittyGritty.Uwp.Services.Activation
{
    public class CommandLineActivationHandler : ActivationHandler<CommandLineActivatedEventArgs>
    {
        private readonly Dictionary<string, CommandLineOperation> operations;

        public CommandLineActivationHandler(CommandLineOperation operation) : base(ActivationStrategy.Normal)
        {
            Operation = operation ?? throw new ArgumentNullException(nameof(operation), "Operation cannot be null");
        }

        public CommandLineOperation Operation { get; }

        protected override async Task HandleInternal(CommandLineActivatedEventArgs args)
        {
            await Operation.Run(args, NavigationContext);
        }
    }
}
