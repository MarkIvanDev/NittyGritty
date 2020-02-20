using System;
using System.Collections.Generic;
using System.Text;
using NittyGritty.Platform.Payloads;
using Windows.ApplicationModel.DataTransfer;

namespace NittyGritty.Services
{
    public partial class ShareService
    {
        private DataTransferManager transferManager = null;
        private ShareData data = null;

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

        void PlatformShare(ShareData data)
        {
            this.data = data;
            DataTransferManager.ShowShareUI();
        }

        private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            if (data != null)
            {
                args.Request.Data.Properties.Title = data.Title;
                args.Request.Data.Properties.Description = data.Description;

                if (data.Text != null) args.Request.Data.SetText(data.Text);
                if (data.Html != null) args.Request.Data.SetHtmlFormat(data.Html);
                if (data.Rtf != null) args.Request.Data.SetRtf(data.Rtf);
                if (data.AppLink != null) args.Request.Data.SetApplicationLink(data.AppLink);
                if (data.WebLink != null) args.Request.Data.SetWebLink(data.WebLink);
                if (data.CustomData.Key != default) args.Request.Data.SetData(data.CustomData.Key, data.CustomData.Value);
                if (data.Files != null || data.Files.Count > 0) args.Request.Data.SetStorageItems(data.Files.Select(f => f.Item as IStorageItem));
                if (data.Bitmap != null)
                {
                    args.Request.Data.SetDataProvider(StandardDataFormats.Bitmap,
                    async (provider) =>
                    {
                        var deferral = provider.GetDeferral();
                        try
                        {
                            var bitmap = await data.Bitmap.GetStream(false);
                            provider.SetData(bitmap);
                        }
                        finally
                        {
                            deferral.Complete();
                        }
                    });
                }
            }
        }
    }
}
