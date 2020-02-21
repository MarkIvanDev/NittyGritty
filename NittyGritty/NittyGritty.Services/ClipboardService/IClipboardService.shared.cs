using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Data;

namespace NittyGritty.Services
{
    public interface IClipboardService
    {
        void Start();

        void Stop();

        void Copy(DataPayload data);

        void Cut(DataPayload data);

        Task<DataPayload> GetData();

        void Clear();

        event EventHandler ContentChanged;
    }
}
