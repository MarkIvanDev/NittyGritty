using System;
using System.Collections.Generic;
using System.Text;
using NittyGritty.Platform.Payloads;

namespace NittyGritty.Services
{
    public partial class ShareService : IShareService
    {
        public void Start()
        {
            PlatformStart();
        }

        public void Stop()
        {
            PlatformStop();
        }

        public void Share(ShareData data)
        {
            PlatformShare(data);
        }

    }
}
