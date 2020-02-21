using System;
using System.Collections.Generic;
using System.Text;
using NittyGritty.Platform.Data;

namespace NittyGritty.Services
{
    public interface IClipboardService
    {
        void Start();

        void Stop();

        void Copy(DataPayload data);

        void Cut(DataPayload data);

        DataPayload GetData();

        void Clear();

        event EventHandler ContentChanged;
    }
}
