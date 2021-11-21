using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Data;
using NittyGritty.Services.Core;

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

        public async Task<DataPayload> GetData()
        {
            return await PlatformGetData();
        }

        public void Clear()
        {
            PlatformClear();
        }

    }
}
