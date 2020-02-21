using NittyGritty.Platform.Contacts;
using NittyGritty.Platform.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.Platform.Data
{
    public interface IShareTargetPayload
    {
        string Title { get; }

        string Description { get; }

        string Id { get; }

        bool HasContacts { get; }

        Task<IReadOnlyCollection<NGContact>> GetContacts();

        #region Data Methods

        Task<DataPayload> GetData();

        Task<T> GetData<T>(string dataFormat) where T : class;

        Task<string> GetText();

        Task<string> GetHtml();

        Task<string> GetRtf();

        Task<Uri> GetAppLink();

        Task<Uri> GetWebLink();

        Task<Stream> GetBitmap();

        Task<IReadOnlyCollection<IStorageItem>> GetStorageItems();

        #endregion

        void Started();

        void Failed(string error);

        void Completed();
    }
}
