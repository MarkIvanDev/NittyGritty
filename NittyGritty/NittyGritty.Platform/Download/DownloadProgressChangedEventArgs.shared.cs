using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Platform.Download
{
    public class DownloadProgressChangedEventArgs
    {
        public DownloadProgressChangedEventArgs(
            string downloadPath, DownloadStatus status = DownloadStatus.Idle, double? total = null, double downloaded = 0)
        {
            DownloadPath = downloadPath;
            Status = status;
            Total = total;
            Downloaded = downloaded;
        }

        public string DownloadPath { get; }

        public DownloadStatus Status { get; }

        public double? Total { get; }

        public double Downloaded { get; }

        public double Percentage => Total.HasValue ? Downloaded / Total.Value : 0;

        public string ErrorMessage { get; set; }
    }

    public delegate void DownloadProgressChangedEventHandler(object sender, DownloadProgressChangedEventArgs args);
}
