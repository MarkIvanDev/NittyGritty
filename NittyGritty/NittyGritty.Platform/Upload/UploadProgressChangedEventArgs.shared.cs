using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Platform.Upload
{
    public class UploadProgressChangedEventArgs
    {
        public UploadProgressChangedEventArgs(
            string uploadPath, UploadStatus status = UploadStatus.Idle, double? total = null, double uploaded = 0)
        {
            UploadPath = uploadPath;
            Status = status;
            Total = total;
            Uploaded = uploaded;
        }

        public string UploadPath { get; }

        public UploadStatus Status { get; }

        public double? Total { get; }

        public double Uploaded { get; }

        public double Percentage => Total.HasValue ? Uploaded / Total.Value : 0;

        public string ErrorMessage { get; set; }
    }

    public delegate void UploadProgressChangedEventHandler(object sender, UploadProgressChangedEventArgs args);
}
