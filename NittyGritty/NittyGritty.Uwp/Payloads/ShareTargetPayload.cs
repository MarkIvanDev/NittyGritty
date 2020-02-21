using NittyGritty.Platform.Contacts;
using NittyGritty.Platform.Payloads;
using NittyGritty.Platform.Storage;
using NittyGritty.Uwp.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer.ShareTarget;

namespace NittyGritty.Uwp.Payloads
{
    public class ShareTargetPayload : IShareTargetPayload
    {
        private readonly ShareOperation operation;

        public ShareTargetPayload(ShareOperation operation)
        {
            this.operation = operation;
            Title = operation.Data.Properties.Title;
            Description = operation.Data.Properties.Description;
            Id = operation.QuickLinkId;
            HasContacts = operation.Contacts.Count > 0;
        }

        public string Title { get; }

        public string Description { get; }

        public string Id { get; }

        public bool HasContacts { get; }

        public async Task<IReadOnlyCollection<NGContact>> GetContacts()
        {
            var contacts = new List<NGContact>();
            foreach (var item in operation.Contacts)
            {
                var contact = await item.ToNGContact();
                contacts.Add(contact);
            }
            return new ReadOnlyCollection<NGContact>(contacts);
        }

        #region Data Methods

        public async Task<DataPayload> GetData()
        {
            var data = await operation.Data.GetData();
            return data;
        }

        public async Task<T> GetData<T>(string dataFormat) where T : class
        {
            var data = await operation.Data.GetData<T>(dataFormat);
            return data;
        }

        public async Task<string> GetText()
        {
            var data = await operation.Data.GetText();
            return data;
        }

        public async Task<string> GetHtml()
        {
            var data = await operation.Data.GetHtml();
            return data;
        }

        public async Task<string> GetRtf()
        {
            var data = await operation.Data.GetRtf();
            return data;
        }

        public async Task<Uri> GetAppLink()
        {
            var data = await operation.Data.GetAppLink();
            return data;
        }

        public async Task<Uri> GetWebLink()
        {
            var data = await operation.Data.GetWebLink();
            return data;
        }

        public async Task<Stream> GetBitmap()
        {
            var data = await operation.Data.GetBitmap();
            return data;
        }

        public async Task<IReadOnlyCollection<IStorageItem>> GetStorageItems()
        {
            var data = await operation.Data.GetStorageItems();
            return data;
        }

        #endregion

        public void Started()
        {
            operation.ReportStarted();
        }

        public void Failed(string error)
        {
            operation.ReportError(error);
        }

        public void Completed()
        {
            operation.ReportCompleted();
        }

    }
}
