using System;
using System.Collections.Generic;
using System.Text;
using NittyGritty.Platform.Data;

namespace NittyGritty.Services
{
    public interface IShareService
    {
        void Start();

        void Stop();

        void Share(DataPayload data);
    }
}
