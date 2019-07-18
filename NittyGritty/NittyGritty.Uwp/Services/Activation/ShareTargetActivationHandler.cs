using NittyGritty.Uwp.Operations;
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
        private static Dictionary<string, ShareTarget> _shareTargets;

        static ShareTargetActivationHandler()
        {
            _shareTargets = new Dictionary<string, ShareTarget>();
        }

        public ShareTargetActivationHandler()
        {
            Strategy = ActivationStrategy.Hosted;
        }

        public static ReadOnlyDictionary<string, ShareTarget> ShareTargets
        {
            get { return new ReadOnlyDictionary<string, ShareTarget>(_shareTargets); }
        }

        public static void AddTarget(ShareTarget target)
        {
            if (!_shareTargets.ContainsKey(target.DataFormat))
            {
                if (!_shareTargets.Values.Any(s => s.Priority == target.Priority) && target.View != null)
                {
                    _shareTargets.Add(target.DataFormat, target);
                }
                else
                {
                    throw new ArgumentException("Share Targets must have unique priorities and views.");
                }
            }
            else
            {
                throw new ArgumentException("You only have to register for a share target once.");
            }
        }

        public override async Task HandleInternal(ShareTargetActivatedEventArgs args)
        {
            var supported = new List<ShareTarget>();
            foreach (var item in args.ShareOperation.Data.AvailableFormats)
            {
                if(_shareTargets.TryGetValue(item, out var target))
                {
                    supported.Add(target);
                }
            }
            var picked = supported.OrderByDescending(s => s.Priority).FirstOrDefault();
            if(picked != null)
            {
                await picked.Run(args.ShareOperation);
            }
            else
            {
                // We should not reach this part. Please check if you have registered all of your share targets
            }
        }

    }

}
