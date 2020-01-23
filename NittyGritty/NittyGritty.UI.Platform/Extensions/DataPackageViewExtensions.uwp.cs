using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Collections.ObjectModel;
using NittyGritty.Platform.Payloads;
using Windows.Storage.Streams;

namespace NittyGritty.UI.Platform.Extensions
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
            var stream = await bitmap?.OpenReadAsync();
            return stream?.AsStream();
        }

        public static async Task<IReadOnlyCollection<Stream>> GetFiles(this DataPackageView data)
        {
            var files = new List<Stream>();
            var storageItems = await GetData<IReadOnlyList<IStorageItem>>(data, StandardDataFormats.StorageItems) ?? new List<IStorageItem>().AsReadOnly();
            foreach (var item in storageItems)
            {
                if(item is IStorageFile storageFile)
                {
                    files.Add(await storageFile.OpenStreamForReadAsync());
                }
                else if (item is IStorageFolder storageFolder)
                {
                    var sf = await storageFolder.GetFilesAsync();
                    foreach (var f in sf)
                    {
                        files.Add(await f.OpenStreamForReadAsync());
                    }
                }
            }
            return new ReadOnlyCollection<Stream>(files);
        }

        public static async Task<T> GetData<T>(this DataPackageView data, string dataFormat)
            where T : class
        {
            try
            {
                return await data.GetDataAsync(dataFormat) as T;
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        //public static async Task<SharePayload> GetPayload(this DataPackageView data, params string[] dataFormats)
        //{
        //    var payload = new SharePayload(data.Properties.Title);
        //    var formats = new List<string>();
        //    if(dataFormats.Length == 0)
        //    {
        //        formats.AddRange(data.AvailableFormats);
        //    }
        //    else
        //    {
        //        formats.AddRange(dataFormats);
        //    }
        //    foreach (var format in formats)
        //    {
        //        if (format == StandardDataFormats.ApplicationLink)
        //        {
        //            payload.SetAppLink(await data.GetAppLink());
        //        }
        //        else if (format == StandardDataFormats.Bitmap)
        //        {
        //            payload.SetBitmap(await data.GetBitmap());
        //        }
        //        else if (format == StandardDataFormats.Html)
        //        {
        //            payload.SetHtml(await data.GetHtml());
        //        }
        //        else if (format == StandardDataFormats.Rtf)
        //        {
        //            payload.SetRtf(await data.GetRtf());
        //        }
        //        else if (format == StandardDataFormats.Text)
        //        {
        //            payload.SetText(await data.GetText());
        //        }
        //        else if (format == StandardDataFormats.WebLink)
        //        {
        //            payload.SetWebLink(await data.GetWebLink());
        //        }
        //        else if (format == StandardDataFormats.StorageItems)
        //        {
        //            payload.SetFiles(await data.GetFiles());
        //        }

        //    }
        //    return payload;
        //}

        public static async Task<ShareData> GetData(this DataPackageView data)
        {
            var shareData = new ShareData();
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

            shareData.StorageItems = await data.GetFiles();
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
