using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Data;
using NittyGritty.Services.Core;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI;

namespace NittyGritty.Uwp.Services
{
    public class ShareService : IShareService
    {
        private DataTransferManager transferManager = null;
        private DataPayload data = null;

        public void Start()
        {
            transferManager = DataTransferManager.GetForCurrentView();
            transferManager.DataRequested += OnDataRequested;
            transferManager.ShareProvidersRequested += OnShareProvidersRequested;
        }

        public void Stop()
        {
            if (transferManager != null)
            {
                transferManager.DataRequested -= OnDataRequested;
                transferManager.ShareProvidersRequested -= OnShareProvidersRequested;
                transferManager = null;
            }
        }

        public void Share(DataPayload data)
        {
            this.data = data;
            DataTransferManager.ShowShareUI();
        }

        private readonly Dictionary<string, ShareProviderBase> shareProviders = new Dictionary<string, ShareProviderBase>();

        public void AddProvider(ShareProviderBase shareProvider)
        {
            if (!shareProviders.TryAdd(shareProvider.Title, shareProvider))
            {
                throw new ArgumentException("You have already registered a ShareProvider with that title");
            }
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
                if (data.Bitmap != null) args.Request.Data.SetBitmap(RandomAccessStreamReference.CreateFromStream(data.Bitmap.AsRandomAccessStream()));
                if (data.StorageItems != null && data.StorageItems.Count > 0) args.Request.Data.SetStorageItems(data.StorageItems.Select(f => f.Context as IStorageItem));
                foreach (var data in data.CustomData ?? new Dictionary<string, object>())
                {
                    args.Request.Data.SetData(data.Key, data.Value);
                }
            }
        }

        private void OnShareProvidersRequested(DataTransferManager sender, ShareProvidersRequestedEventArgs args)
        {
            var deferral = args.GetDeferral();
            foreach (var provider in shareProviders)
            {
                if (args.Data.AvailableFormats.Intersect(provider.Value.SupportedFormats).Any())
                {
                    args.Providers.Add(provider.Value.GetShareProvider());
                }
            }
            deferral.Complete();
        }
    }

    public abstract class ShareProviderBase
    {
        public ShareProviderBase(string title, RandomAccessStreamReference icon, Color backgroundColor, IList<string> supportedFormats)
        {
            Title = title;
            Icon = icon;
            BackgroundColor = backgroundColor;
            SupportedFormats = new ReadOnlyCollection<string>(supportedFormats);
        }

        public string Title { get; }

        public RandomAccessStreamReference Icon { get; }

        public Color BackgroundColor { get; }

        public ReadOnlyCollection<string> SupportedFormats { get; }

        public abstract void OnShare(ShareProviderOperation operation);

        public ShareProvider GetShareProvider()
        {
            return new ShareProvider(Title, Icon, BackgroundColor, OnShare);
        }
    }
}
