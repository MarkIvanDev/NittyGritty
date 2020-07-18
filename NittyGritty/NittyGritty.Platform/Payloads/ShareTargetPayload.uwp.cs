using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Contacts;
using NittyGritty.Platform.Data;
using NittyGritty.Platform.Storage;
using Windows.ApplicationModel.DataTransfer.ShareTarget;

namespace NittyGritty.Platform.Payloads
{
    public class ShareTargetPayload : IShareTargetPayload
    {
        public ShareTargetPayload(ShareOperation operation)
        {
            Operation = operation;
            Title = operation.Data.Properties.Title;
            Description = operation.Data.Properties.Description;
            Id = operation.QuickLinkId;
            HasContacts = operation.Contacts.Count > 0;
        }

        public ShareOperation Operation { get; }

        public string Title { get; }

        public string Description { get; }

        public string Id { get; }

        public bool HasContacts { get; }

        public async Task<IReadOnlyCollection<NGContact>> GetContacts()
        {
            var contacts = new List<NGContact>();
            foreach (var item in Operation.Contacts)
            {
                var contact = await item.ToNGContact();
                contacts.Add(contact);
            }
            return new ReadOnlyCollection<NGContact>(contacts);
        }

        #region Data Methods

        public async Task<DataPayload> GetData()
        {
            var data = await Operation.Data.GetData();
            return data;
        }

        public async Task<T> GetData<T>(string dataFormat) where T : class
        {
            var data = await Operation.Data.GetData<T>(dataFormat);
            return data;
        }

        public async Task<string> GetText()
        {
            var data = await Operation.Data.GetText();
            return data;
        }

        public async Task<string> GetHtml()
        {
            var data = await Operation.Data.GetHtml();
            return data;
        }

        public async Task<string> GetRtf()
        {
            var data = await Operation.Data.GetRtf();
            return data;
        }

        public async Task<Uri> GetAppLink()
        {
            var data = await Operation.Data.GetAppLink();
            return data;
        }

        public async Task<Uri> GetWebLink()
        {
            var data = await Operation.Data.GetWebLink();
            return data;
        }

        public async Task<Stream> GetBitmap()
        {
            var data = await Operation.Data.GetBitmap();
            return data;
        }

        public async Task<IReadOnlyCollection<IStorageItem>> GetStorageItems()
        {
            var data = await Operation.Data.GetStorageItems();
            return data;
        }

        #endregion

        public void Started()
        {
            Operation.ReportStarted();
        }

        public void Failed(string error)
        {
            Operation.ReportError(error);
        }

        public void Completed()
        {
            Operation.ReportCompleted();
        }

    }
}
