﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Data;

namespace NittyGritty.Services
{
    public partial class ClipboardService
    {
        void PlatformStart()
        {
            throw new NotImplementedException();
        }

        void PlatformStop()
        {
            throw new NotImplementedException();
        }

        void PlatformCopy(DataPayload data)
        {
            throw new NotImplementedException();
        }

        void PlatformCut(DataPayload data)
        {
            throw new NotImplementedException();
        }

        Task<DataPayload> PlatformGetData()
        {
            throw new NotImplementedException();
        }

        void PlatformClear()
        {
            throw new NotImplementedException();
        }
    }
}
