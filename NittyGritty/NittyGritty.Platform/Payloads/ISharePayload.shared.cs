using NittyGritty.Platform.Contacts;
using NittyGritty.Platform.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.Platform.Payloads
{
    public interface ISharePayload
    {
        IReadOnlyCollection<NGContact> Contacts { get; }

        string Title { get; }

        Task<string> GetText();

        Task<string> GetHtml();

        Task<string> GetRtf();

        Task<Uri> GetAppLink();

        Task<Uri> GetWeblink();

        Task<Stream> GetBitmap();

        Task<IReadOnlyCollection<IStorageItem>> GetStorageItems();

        Task<object> GetCustomData(string format);
    }
}
