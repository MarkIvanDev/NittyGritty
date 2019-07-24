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
    public class ProtocolActivationHandler : ActivationHandler<ProtocolActivatedEventArgs>
    {
        private readonly Dictionary<string, ProtocolOperation> operations;

        private ProtocolActivationHandler(ActivationStrategy strategy, params ProtocolOperation[] operations) : base(strategy)
        {
            this.operations = new Dictionary<string, ProtocolOperation>();
            foreach (var operation in operations)
            {
                this.operations.Add(operation.Scheme, operation);
            }
            Operations = new ReadOnlyDictionary<string, ProtocolOperation>(this.operations);
        }

        public ProtocolActivationHandler(params ProtocolOperation[] operations) : this(ActivationStrategy.Normal, operations)
        {
            
        }

        public ReadOnlyDictionary<string, ProtocolOperation> Operations { get; }

        protected override async Task HandleInternal(ProtocolActivatedEventArgs args)
        {
            if(operations.TryGetValue(args.Uri.Scheme, out var operation))
            {
                await operation.Run(args, NavigationContext);
            }
            else
            {
                // We should not reach this part. Please check if you have registered all of the Protocols this app handles
            }
        }
    }
}
