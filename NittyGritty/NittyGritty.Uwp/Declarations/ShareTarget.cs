using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation.Metadata;

namespace NittyGritty.Uwp.Declarations
{
    public abstract class ShareTarget
    {
        private static HashSet<string> supportedDataFormats = new HashSet<string>()
        {
            StandardDataFormats.ApplicationLink,
            StandardDataFormats.Bitmap,
            StandardDataFormats.Html,
            StandardDataFormats.Rtf,
            StandardDataFormats.StorageItems,
            StandardDataFormats.Text,
            StandardDataFormats.WebLink
        };

        static ShareTarget()
        {
            if (ApiInformation.IsReadOnlyPropertyPresent("Windows.ApplicationModel.DataTransfer.StandardDataFormats", "UserActivityJsonArray"))
            {
                supportedDataFormats.Add(StandardDataFormats.UserActivityJsonArray);
            }
        }

        public ShareTarget(string dataFormat, Type view) : this(dataFormat, Enumerable.Empty<string>().ToList(), view)
        {
        }

        public ShareTarget(IEnumerable<string> fileTypes, Type view) : this(StandardDataFormats.StorageItems, fileTypes, view)
        {
        }

        private ShareTarget(string dataFormat, IEnumerable<string> fileTypes, Type view)
        {
            if(!supportedDataFormats.Contains(dataFormat))
            {
                throw new ArgumentException("Data Format is not supported");
            }

            DataFormat = dataFormat;
            FileTypes = new ReadOnlyCollection<string>(fileTypes.ToList());
            View = view;
        }

        public string DataFormat { get; }

        /// <summary>
        /// If Data Format is StorageItems, this will contain the supported File Types. If empty, it means it supports any file type
        /// </summary>
        public ReadOnlyCollection<string> FileTypes { get; }

        public Type View { get; }

        public async Task Run(Uri deepLink)
        {
            if (deepLink.Scheme != Scheme)
            {
                return;
            }

            var path = deepLink.LocalPath;
            var parameters = QueryString.Parse(deepLink.GetComponents(UriComponents.Query, UriFormat.UriEscaped));
            await Process(Scheme, path, parameters);
        }

        protected abstract Task Process(string scheme, string path, QueryString parameters);

    }
}
