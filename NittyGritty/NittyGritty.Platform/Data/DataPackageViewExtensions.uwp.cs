using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Data;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage.Streams;
using NGStorage = NittyGritty.Platform.Storage;
using WinStorage = Windows.Storage;

namespace NittyGritty.Platform.Data
{
    public static class DataPackageViewExtensions
    {
        public static async Task<string> GetText(this DataPackageView data)
        {
            return await GetData<string>(data, StandardDataFormats.Text);
        }

        public static async Task<string> GetHtml(this DataPackageView data)
        {
            return await GetData<string>(data, StandardDataFormats.Html);
        }

        public static async Task<string> GetRtf(this DataPackageView data)
        {
            return await GetData<string>(data, StandardDataFormats.Rtf);
        }

        public static async Task<Uri> GetAppLink(this DataPackageView data)
        {
            return await GetData<Uri>(data, StandardDataFormats.ApplicationLink);
        }

        public static async Task<Uri> GetWebLink(this DataPackageView data)
        {
            return await GetData<Uri>(data, StandardDataFormats.WebLink);
        }

        public static async Task<Stream> GetBitmap(this DataPackageView data)
        {
            var bitmap = await GetData<RandomAccessStreamReference>(data, StandardDataFormats.Bitmap);
            if(bitmap != null)
            {
                var stream = await bitmap.OpenReadAsync();
                return stream?.AsStream();
            }
            return default(Stream);
        }

        public static async Task<IReadOnlyCollection<NGStorage.IStorageItem>> GetStorageItems(this DataPackageView data)
        {
            var items = new List<NGStorage.IStorageItem>();
            var storageItems = await GetData<IReadOnlyList<WinStorage.IStorageItem>>(data, StandardDataFormats.StorageItems) ?? new List<WinStorage.IStorageItem>().AsReadOnly();
            foreach (var item in storageItems)
            {
                if (item is WinStorage.IStorageFile storageFile)
                {
                    items.Add(new NGStorage.NGFile(storageFile));
                }
                else if (item is WinStorage.IStorageFolder storageFolder)
                {
                    items.Add(new NGStorage.NGFolder(storageFolder));
                }
            }
            return new ReadOnlyCollection<NGStorage.IStorageItem>(items);
        }

        public static async Task<T> GetData<T>(this DataPackageView data, string dataFormat)
            where T : class
        {
            try
            {
                if(data.Contains(dataFormat))
                {
                    return await data.GetDataAsync(dataFormat) as T;
                }
                return default(T);
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        public static async Task<DataPayload> GetData(this DataPackageView data)
        {
            var shareData = new DataPayload
            {
                Title = data.Properties.Title,
                Description = data.Properties.Description
            };

            var formats = data.AvailableFormats.ToList();

            shareData.AppLink = await data.GetAppLink();
            formats.Remove(StandardDataFormats.ApplicationLink);

            shareData.Bitmap = await data.GetBitmap();
            formats.Remove(StandardDataFormats.Bitmap);

            shareData.Html = await data.GetHtml();
            formats.Remove(StandardDataFormats.Html);

            shareData.Rtf = await data.GetRtf();
            formats.Remove(StandardDataFormats.Rtf);

            shareData.Text = await data.GetText();
            formats.Remove(StandardDataFormats.Text);

            shareData.WebLink = await data.GetWebLink();
            formats.Remove(StandardDataFormats.WebLink);

            shareData.StorageItems = await data.GetStorageItems();
            formats.Remove(StandardDataFormats.StorageItems);

            // Since we removed all the built-in data formats, the remaining will be the custom data formats that the developer has included
            var customData = new Dictionary<string, object>();
            foreach (var customFormat in formats)
            {
                var d = await data.GetDataAsync(customFormat);
                customData.Add(customFormat, d);
            }
            shareData.CustomData = new ReadOnlyDictionary<string, object>(customData);

            return shareData;
        }

    }
}
