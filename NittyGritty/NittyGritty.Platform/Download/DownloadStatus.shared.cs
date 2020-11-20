using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Platform.Download
{
    public enum DownloadStatus
    {
        Unknown = 0,
        Idle = 1,
        Running = 2,
        Paused = 3,
        Completed = 4,
        Canceled = 5,
        Error = 6
    }
}
