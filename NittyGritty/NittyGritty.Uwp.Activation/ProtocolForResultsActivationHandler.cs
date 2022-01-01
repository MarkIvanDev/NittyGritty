using NittyGritty.Uwp.Activation.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp.Activation
{
    public class ProtocolForResultsActivationHandler : ActivationHandler<ProtocolForResultsActivatedEventArgs>
    {
        private readonly Dictionary<string, ProtocolForResultsOperation> operations;

        public ProtocolForResultsActivationHandler(params ProtocolForResultsOperation[] operations) : base(ActivationStrategy.Hosted)
        {
            this.operations = new Dictionary<string, ProtocolForResultsOperation>();
            foreach (var operation in operations)
            {
                this.operations.Add(operation.Scheme, operation);
            }
        }

        protected override async Task HandleInternal(ProtocolForResultsActivatedEventArgs args)
        {
            if (operations.TryGetValue(args.Uri.Scheme, out var operation))
            {
                await operation.Run(args, Window.Current.Content as Frame);
                //if(Window.Current.Content is Frame frame)
                //{
                //    if(frame.Content is Page page)
                //    {
                //        if(page.DataContext is IProtocolResult context)
                //        {
                //            context.ProtocolResultCompleted += (s, e) =>
                //            {
                //                var data = new ValueSet();
                //                foreach (var item in e.Result)
                //                {
                //                    data.Add(item.Key, item.Value);
                //                }
                //                args.ProtocolForResultsOperation.ReportCompleted(data);
                //            };
                //        }
                //    }
                //}
            }
            else
            {
                // We should not reach this part. Please check if you have registered all of the Protocols this app handles
            }
        }

    }
}
