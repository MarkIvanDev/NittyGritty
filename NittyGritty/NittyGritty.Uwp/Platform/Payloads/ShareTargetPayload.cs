using NittyGritty.Platform.Contacts;
using NittyGritty.Platform.Payloads;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.DataTransfer.ShareTarget;

namespace NittyGritty.Uwp.Platform.Payloads
{
    public class ShareTargetPayload : IShareTargetPayload
    {
        private readonly ShareOperation operation;
        private readonly DataPackageView data;

        public ShareTargetPayload(ShareOperation operation, DataPackageView data, IList<NGContact> contacts = null)
        {
            this.operation = operation;
            this.data = data;
            Title = data.Properties.Title;
            Description = data.Properties.Description;
            Id = operation.QuickLinkId;
            Contacts = new ReadOnlyCollection<NGContact>(contacts ?? Enumerable.Empty<NGContact>().ToList());
        }

        public string Title { get; }

        public string Description { get; }

        public string Id { get; }

        public IReadOnlyCollection<NGContact> Contacts { get; }

        public void ShareStarted()
        {
            operation.ReportStarted();
        }

        public void ShareFailed(string error)
        {
            operation.ReportError(error);
        }

        public void ShareCompleted()
        {
            operation.ReportCompleted();
        }

    }
}
