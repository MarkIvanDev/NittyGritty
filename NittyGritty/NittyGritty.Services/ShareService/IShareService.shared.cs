using System;
using System.Collections.Generic;
using System.Text;
using NittyGritty.Platform.Payloads;

namespace NittyGritty.Services
{
    public interface IShareService
    {
        void Start();

        void Stop();

        void Share(ShareData data);
    }
}
