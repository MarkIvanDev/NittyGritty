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

        private CommandLineActivationHandler(ActivationStrategy strategy, params CommandLineOperation[] operations) : base(strategy)
        {
            this.operations = new Dictionary<string, CommandLineOperation>();
            foreach (var operation in operations)
            {
                this.operations.Add(operation.Command, operation);
            }
            Operations = new ReadOnlyDictionary<string, CommandLineOperation>(this.operations);
        }

        public CommandLineActivationHandler(params CommandLineOperation[] operations) : this(ActivationStrategy.Normal, operations)
        {

        }

        public ReadOnlyDictionary<string, CommandLineOperation> Operations { get; }

        protected override async Task HandleInternal(CommandLineActivatedEventArgs args)
        {
            var deferral = args.Operation.GetDeferral();
            
            deferral.Complete();
        }
    }
}
