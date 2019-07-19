using NittyGritty.Uwp.Operations;
using NittyGritty.Views;
using NittyGritty.Views.Events;
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

namespace NittyGritty.Uwp.Services.Activation
{
    public class ShareTargetActivationHandler : ActivationHandler<ShareTargetActivatedEventArgs>
    {
        private readonly Dictionary<string, ShareTargetOperation> operations;

        public ShareTargetActivationHandler(params ShareTargetOperation[] operations) : base(ActivationStrategy.Hosted)
        {
            this.operations = new Dictionary<string, ShareTargetOperation>();
            foreach (var operation in operations)
            {
                this.operations.Add(operation.DataFormat, operation);
            }
            Operations = new ReadOnlyDictionary<string, ShareTargetOperation>(this.operations);
        }

        public ReadOnlyDictionary<string, ShareTargetOperation> Operations { get; }

        protected override async Task HandleInternal(ShareTargetActivatedEventArgs args)
        {
            var supported = new List<ShareTargetOperation>();
            foreach (var item in args.ShareOperation.Data.AvailableFormats)
            {
                if(operations.TryGetValue(item, out var target))
                {
                    supported.Add(target);
                }
            }
            var picked = supported.OrderByDescending(s => s.Priority).ThenBy(s => s.DataFormat).FirstOrDefault();
            if(picked != null)
            {
                await picked.Run(args);
                if (Window.Current.Content is Frame frame)
                {
                    if (frame.Content is Page page)
                    {
                        if (page.DataContext is IShareTarget context)
                        {
                            context.ShareStarted += (s, e) => args.ShareOperation.ReportStarted();
                            context.ShareFailed += (s, e) => args.ShareOperation.ReportError(e.Error);
                            context.ShareCompleted += (s, e) => args.ShareOperation.ReportCompleted();
                        }
                    }
                }
            }
            else
            {
                // We should not reach this part. Please check if you have registered all of your share targets
            }
        }
    }

}
