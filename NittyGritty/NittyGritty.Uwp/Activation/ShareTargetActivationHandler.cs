using NittyGritty.Uwp.Activation.Operations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.DataTransfer.ShareTarget;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp.Activation
{
    public class ShareTargetActivationHandler : ActivationHandler<ShareTargetActivatedEventArgs>
    {
        private readonly Dictionary<ShareTargetType, ShareTargetOperation> operations;

        public ShareTargetActivationHandler(params ShareTargetOperation[] operations) : base(ActivationStrategy.Hosted)
        {
            this.operations = new Dictionary<ShareTargetType, ShareTargetOperation>();
            foreach (var operation in operations)
            {
                this.operations.Add(operation.Type, operation);
            }
        }

        protected override async Task HandleInternal(ShareTargetActivatedEventArgs args)
        {
            if(args.ShareOperation.Contacts.Count > 0)
            {
                if(operations.TryGetValue(ShareTargetType.HasContacts, out var operation))
                {
                    await operation.Run(args, Window.Current.Content as Frame);
                }
            }
            else
            {
                if (operations.TryGetValue(ShareTargetType.Regular, out var operation))
                {
                    await operation.Run(args, Window.Current.Content as Frame);
                }
            }
        }
    }

}
