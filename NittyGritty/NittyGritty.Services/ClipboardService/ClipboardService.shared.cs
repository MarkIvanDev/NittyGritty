using System;
using System.Collections.Generic;
using System.Text;
using NittyGritty.Platform.Data;

namespace NittyGritty.Services
{
    public partial class ClipboardService : IClipboardService
    {
        public event EventHandler ContentChanged;

        public void Start()
        {
            PlatformStart();
        }

        public void Stop()
        {
            PlatformStop();
        }

        public void Copy(DataPayload data)
        {
            PlatformCopy(data);
        }

        public void Cut(DataPayload data)
        {
            PlatformCut(data);
        }

        public DataPayload GetData()
        {
            return PlatformGetData();
        }

        public void Clear()
        {
            PlatformClear();
        }

    }
}
