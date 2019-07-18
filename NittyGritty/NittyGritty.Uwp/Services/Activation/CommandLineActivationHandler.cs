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
        private readonly CommandLineOperation operation;

        public CommandLineActivationHandler(CommandLineOperation operation) : base(ActivationStrategy.Normal)
        {
            this.operation = operation;
        }

        protected override async Task HandleInternal(CommandLineActivatedEventArgs args)
        {
            var deferral = args.Operation.GetDeferral();
            await operation.Run(args, NavigationContext);
            deferral.Complete();
        }
    }
}
