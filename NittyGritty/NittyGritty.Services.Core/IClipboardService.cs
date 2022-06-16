using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Data;

namespace NittyGritty.Services.Core
{
    public interface IClipboardService
    {
        void Start();

        void Stop();

        void Copy(DataPayload data);

        void Cut(DataPayload data);

        Task<DataPayload> GetData();

        bool ContainsText();

        bool ContainsBitmap();

        bool ContainsHtml();

        bool ContainsRtf();

        bool ContainsStorageItems();

        bool ContainsAppLink();

        bool ContainsWebLink();

        bool ContainsData(string formatId);

        void Clear();

        event EventHandler ContentChanged;
    }
}
