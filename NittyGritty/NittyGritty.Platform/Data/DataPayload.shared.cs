using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NittyGritty.Platform.Storage;
#if UWP
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage.Streams;
#endif

namespace NittyGritty.Platform.Data
{
    public class DataPayload : ObservableObject
    {

        private string _title;

        public string Title
        {
            get { return _title; }
            set { Set(ref _title, value); }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { Set(ref _description, value); }
        }

        private string _text;

        public string Text
        {
            get { return _text; }
            set { Set(ref _text, value); }
        }

        private string _html;

        public string Html
        {
            get { return _html; }
            set { Set(ref _html, value); }
        }

        private string _rtf;

        public string Rtf
        {
            get { return _rtf; }
            set { Set(ref _rtf, value); }
        }

        private Uri _appLink;

        public Uri AppLink
        {
            get { return _appLink; }
            set { Set(ref _appLink, value); }
        }

        private Uri _webLink;

        public Uri WebLink
        {
            get { return _webLink; }
            set { Set(ref _webLink, value); }
        }

        private Stream _bitmap;

        public Stream Bitmap
        {
            get { return _bitmap; }
            set { Set(ref _bitmap, value); }
        }

        private IReadOnlyDictionary<string, object> _customData;

        public IReadOnlyDictionary<string, object> CustomData
        {
            get { return _customData; }
            set { Set(ref _customData, value); }
        }

        private IReadOnlyCollection<IStorageItem> _storageItems;

        public IReadOnlyCollection<IStorageItem> StorageItems
        {
            get { return _storageItems; }
            set { Set(ref _storageItems, value); }
        }

#if UWP
        public DataPackage AsDataPackage()
        {
            var dataPackage = new DataPackage();
            if (Title != null) dataPackage.Properties.Title = Title;
            if (Description != null) dataPackage.Properties.Description = Description;

            if (Text != null) dataPackage.SetText(Text);
            if (Html != null) dataPackage.SetHtmlFormat(Html);
            if (Rtf != null) dataPackage.SetRtf(Rtf);
            if (AppLink != null) dataPackage.SetApplicationLink(AppLink);
            if (WebLink != null) dataPackage.SetWebLink(WebLink);
            if (Bitmap != null) dataPackage.SetBitmap(RandomAccessStreamReference.CreateFromStream(Bitmap.AsRandomAccessStream()));
            if (StorageItems != null && StorageItems.Count > 0) dataPackage.SetStorageItems(StorageItems.Select(f => f.Context as Windows.Storage.IStorageItem));
            if (CustomData != null && CustomData.Count > 0)
            {
                foreach (var data in CustomData)
                {
                    dataPackage.SetData(data.Key, data.Value);
                }
            }
            
            return dataPackage;
        }
#endif
    }
}
