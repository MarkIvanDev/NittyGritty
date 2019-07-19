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

namespace NittyGritty.Uwp.Services.Activation
{
    public class CommandLineActivationHandler : ActivationHandler<CommandLineActivatedEventArgs>
    {
        private readonly Dictionary<string, CommandLineOperation> operations;

        public CommandLineActivationHandler(params CommandLineOperation[] operations) : base(ActivationStrategy.Normal)
        {
            this.operations = new Dictionary<string, CommandLineOperation>();
            foreach (var operation in operations)
            {
                this.operations.Add(operation.Command, operation);
            }
            Operations = new ReadOnlyDictionary<string, CommandLineOperation>(this.operations);
        }

        public ReadOnlyDictionary<string, CommandLineOperation> Operations { get; }

        protected override async Task HandleInternal(CommandLineActivatedEventArgs args)
        {
            var deferral = args.Operation.GetDeferral();
            await operations.Run(args, NavigationContext);
            deferral.Complete();
        }
    }
}
