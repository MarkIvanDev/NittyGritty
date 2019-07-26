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
using NittyGritty.Views.Payloads;

namespace NittyGritty.Uwp.Extensions
{
    public static class DataPackageViewExtensions
    {

        public static async Task<string> GetText(this DataPackageView data)
        {
            return await data.GetTextAsync();
        }

        public static async Task<string> GetHtml(this DataPackageView data)
        {
            return await data.GetHtmlFormatAsync();
        }

        public static async Task<string> GetRtf(this DataPackageView data)
        {
            return await data.GetRtfAsync();
        }

        public static async Task<Uri> GetAppLink(this DataPackageView data)
        {
            return await data.GetApplicationLinkAsync();
        }

        public static async Task<Uri> GetWebLink(this DataPackageView data)
        {
            return await data.GetWebLinkAsync();
        }

        public static async Task<Stream> GetBitmap(this DataPackageView data)
        {
            var bitmap = await data.GetBitmapAsync();
            var stream = await bitmap.OpenReadAsync();
            return stream.AsStream();
        }

        public static async Task<ReadOnlyCollection<Stream>> GetFiles(this DataPackageView data)
        {
            var files = new List<Stream>();
            var dataFiles = await data.GetStorageItemsAsync();
            foreach (var file in dataFiles)
            {
                if(file is IStorageFile storageFile)
                {
                    files.Add(await storageFile.OpenStreamForReadAsync());
                }
            }
            return new ReadOnlyCollection<Stream>(files);
        }

        private static async Task<T> GetData<T>(this DataPackageView data, string dataFormat)
            where T : class
        {
            try
            {
                if (dataFormat == StandardDataFormats.ApplicationLink)
                {
                    return await data.GetApplicationLinkAsync() as T;
                }

                if (dataFormat == StandardDataFormats.Bitmap)
                {
                    return await data.GetBitmapAsync() as T;
                }

                if (dataFormat == StandardDataFormats.Html)
                {
                    return await data.GetHtmlFormatAsync() as T;
                }

                if (dataFormat == StandardDataFormats.Rtf)
                {
                    return await data.GetRtfAsync() as T;
                }

                if (dataFormat == StandardDataFormats.StorageItems)
                {
                    return await data.GetStorageItemsAsync() as T;
                }

                if (dataFormat == StandardDataFormats.Text)
                {
                    return await data.GetTextAsync() as T;
                }

                if (dataFormat == StandardDataFormats.WebLink)
                {
                    return await data.GetWebLinkAsync() as T;
                }
            }
            catch (Exception)
            {
                return default(T);
            }

            return default(T);
        }

        public static async Task<SharePayload> GetPayload(this DataPackageView data, params string[] dataFormats)
        {
            var payload = new SharePayload(data.Properties.Title);
            var formats = new List<string>();
            if(dataFormats.Length == 0)
            {
                formats.AddRange(data.AvailableFormats);
            }
            foreach (var format in formats)
            {
                if (format == StandardDataFormats.ApplicationLink)
                {
                    payload.SetAppLink(await data.GetAppLink());
                }
                else if (format == StandardDataFormats.Bitmap)
                {
                    payload.SetBitmap(await data.GetBitmap());
                }
                else if (format == StandardDataFormats.Html)
                {
                    payload.SetHtml(await data.GetHtml());
                }
                else if (format == StandardDataFormats.Rtf)
                {
                    payload.SetRtf(await data.GetRtf());
                }
                else if (format == StandardDataFormats.Text)
                {
                    payload.SetText(await data.GetText());
                }
                else if (format == StandardDataFormats.WebLink)
                {
                    payload.SetWebLink(await data.GetWebLink());
                }
                else if (format == StandardDataFormats.StorageItems)
                {
                    payload.SetFiles(await data.GetFiles());
                }

            }
            return payload;
        }
    }
}
