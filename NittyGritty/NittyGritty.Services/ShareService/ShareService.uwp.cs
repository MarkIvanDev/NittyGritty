using System;
using System.Collections.Generic;
using System.Text;
using NittyGritty.Platform.Data;
using Windows.ApplicationModel.DataTransfer;

namespace NittyGritty.Services
{
    public partial class ShareService
    {
        private DataTransferManager transferManager = null;
        private DataPayload data = null;

        void PlatformStart()
        {
            transferManager = DataTransferManager.GetForCurrentView();
            transferManager.DataRequested += OnDataRequested;
        }

        void PlatformStop()
        {
            if (transferManager != null)
            {
                transferManager.DataRequested -= OnDataRequested;
                transferManager = null;
            }
        }

        void PlatformShare(DataPayload data)
        {
            this.data = data;
            DataTransferManager.ShowShareUI();
        }

        private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            if (data != null)
            {
                args.Request.Data = data.AsDataPackage();
                //args.Request.Data.Properties.Title = data.Title;
                //args.Request.Data.Properties.Description = data.Description;

                //if (data.Text != null) args.Request.Data.SetText(data.Text);
                //if (data.Html != null) args.Request.Data.SetHtmlFormat(data.Html);
                //if (data.Rtf != null) args.Request.Data.SetRtf(data.Rtf);
                //if (data.AppLink != null) args.Request.Data.SetApplicationLink(data.AppLink);
                //if (data.WebLink != null) args.Request.Data.SetWebLink(data.WebLink);
                //if (data.Bitmap != null) args.Request.Data.SetBitmap(RandomAccessStreamReference.CreateFromStream(data.Bitmap.AsRandomAccessStream()));
                //if (data.StorageItems != null) args.Request.Data.SetStorageItems(data.StorageItems.Select(f => f.Context as IStorageItem));
                //foreach (var data in data.CustomData)
                //{
                //    args.Request.Data.SetData(data.Key, data.Value);
                //}
            }
        }
    }
}
