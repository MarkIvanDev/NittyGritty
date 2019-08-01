using NittyGritty.Uwp.Platform;
using NittyGritty.Uwp.Platform.Payloads;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;

namespace NittyGritty.Uwp.Activation.Operations.Configurations
{
    public class ShareTargetConfiguration
    {
        public ShareTargetConfiguration(string dataFormat, SingleViewConfiguration<ShareTargetPayload> view, int priority)
            : this(dataFormat, Enumerable.Empty<string>(), view, priority)
        {
        }

        public ShareTargetConfiguration(IEnumerable<string> fileTypes, SingleViewConfiguration<ShareTargetPayload> view, int priority)
            : this(StandardDataFormats.StorageItems, fileTypes, view, priority)
        {

        }

        private ShareTargetConfiguration(string dataFormat, IEnumerable<string> fileTypes, SingleViewConfiguration<ShareTargetPayload> view, int priority)
        {
            if (string.IsNullOrWhiteSpace(dataFormat))
            {
                throw new ArgumentException("Data Format cannot be null, empty, or whitespace", nameof(dataFormat));
            }

            if (fileTypes == null)
            {
                throw new ArgumentNullException(nameof(fileTypes));
            }

            if (dataFormat != StandardDataFormats.StorageItems && fileTypes.Any())
            {
                throw new ArgumentException("Only the StorageItems Data Format can have supported file types");
            }

            DataFormat = dataFormat;
            FileTypes = new ReadOnlyCollection<string>(fileTypes.ToList());
            View = view ?? throw new ArgumentNullException(nameof(view));
            Priority = priority;
        }

        public string DataFormat { get; }

        public ReadOnlyCollection<string> FileTypes { get; }

        public SingleViewConfiguration<ShareTargetPayload> View { get; }

        public int Priority { get; }
    }
}
