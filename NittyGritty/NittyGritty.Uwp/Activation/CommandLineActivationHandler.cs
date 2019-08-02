using NittyGritty.Uwp.Activation.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;

namespace NittyGritty.Uwp.Activation
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
            await operation.Run(args, NavigationContext);
        }
    }
}
