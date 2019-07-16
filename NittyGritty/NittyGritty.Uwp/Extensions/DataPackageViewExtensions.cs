using NittyGritty.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.Storage.Streams;

namespace NittyGritty.Uwp.Extensions
{
    public static class DataPackageViewExtensions
    {

        public static async Task<string> GetTextAsync(this DataPackageView data)
        {
            return await GetData<string>(data, StandardDataFormats.Text);
        }

        public static async Task<string> GetHtmlAsync(this DataPackageView data)
        {
            return await GetData<string>(data, StandardDataFormats.Html);
        }

        public static async Task<string> GetRtfAsync(this DataPackageView data)
        {
            return await GetData<string>(data, StandardDataFormats.Rtf);
        }

        public static async Task<Uri> GetAppLinkAsync(this DataPackageView data)
        {
            return await GetData<Uri>(data, StandardDataFormats.ApplicationLink);
        }

        public static async Task<Uri> GetWebLinkAsync(this DataPackageView data)
        {
            return await GetData<Uri>(data, StandardDataFormats.WebLink);
        }

        public static async Task<RandomAccessStreamReference> GetBitmapAsync(this DataPackageView data)
        {
            return await GetData<RandomAccessStreamReference>(data, StandardDataFormats.Bitmap);
        }

        public static async Task<IReadOnlyList<IStorageItem>> GetFilesAsync(this DataPackageView data)
        {
            return await GetData<IReadOnlyList<IStorageItem>>(data, StandardDataFormats.StorageItems);
        }

        public static async Task<T> GetData<T>(this DataPackageView data, string dataFormat)
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

    }
}
