using NittyGritty.Uwp.Declarations;
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
        private ShareOperation shareOperation;
        private static Dictionary<string, ShareTarget> _shareTargets;

        static ShareTargetActivationHandler()
        {
            _shareTargets = new Dictionary<string, ShareTarget>();
        }

        public ShareTargetActivationHandler()
        {
            Strategy = ActivationStrategy.Picker;
            SetDefaultDataFormatPicker();
        }

        public static ReadOnlyDictionary<string, ShareTarget> ShareTargets
        {
            get { return new ReadOnlyDictionary<string, ShareTarget>(_shareTargets); }
        }

        public static void AddTarget(ShareTarget target)
        {
            if (!_shareTargets.ContainsKey(target.DataFormat))
            {
                _shareTargets.Add(target.DataFormat, target);
            }
            else
            {
                throw new ArgumentException("You only have to register for a share target once.");
            }
        }

        public Func<IEnumerable<string>, string> DataFormatPicker { get; set; }

        public override async Task HandleAsync(ShareTargetActivatedEventArgs args)
        {
            shareOperation = args.ShareOperation;

            // Since this is marked as a Picker activation, it is assumed that the current window's content has been initialized with a frame by the ActivationService
            if (Window.Current.Content is Frame frame)
            {
                
            }
        }

        public void SetDefaultDataFormatPicker()
        {
            DataFormatPicker = (available) =>
            {
                var supported = _shareTargets.Keys.Intersect(available).ToHashSet();
                if (supported.Contains(StandardDataFormats.StorageItems))
                {
                    return StandardDataFormats.StorageItems;
                }

                if (supported.Contains(StandardDataFormats.Bitmap))
                {
                    return StandardDataFormats.Bitmap;
                }

                if (supported.Contains(StandardDataFormats.Rtf))
                {
                    return StandardDataFormats.Rtf;
                }

                if (supported.Contains(StandardDataFormats.Html))
                {
                    return StandardDataFormats.Html;
                }

                if (supported.Contains(StandardDataFormats.WebLink))
                {
                    return StandardDataFormats.WebLink;
                }

                if (supported.Contains(StandardDataFormats.ApplicationLink))
                {
                    return StandardDataFormats.ApplicationLink;
                }

                return StandardDataFormats.Text;
            };
        }
    }
}
