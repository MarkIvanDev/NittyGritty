using NittyGritty.Utilities;
using NittyGritty.Uwp.Extensions;
using NittyGritty.Views;
using NittyGritty.Views.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.DataTransfer.ShareTarget;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp.Operations
{
    public abstract class ShareTargetOperation
    {
        
        #region Static Constructor and Fields

        private static HashSet<string> supportedDataFormats = new HashSet<string>()
        {
            StandardDataFormats.Text,
            StandardDataFormats.Html,
            StandardDataFormats.Rtf,
            StandardDataFormats.ApplicationLink,
            StandardDataFormats.WebLink,
            StandardDataFormats.Bitmap,
            StandardDataFormats.StorageItems
        };

        static ShareTargetOperation()
        {
            if (ApiInformation.IsReadOnlyPropertyPresent("Windows.ApplicationModel.DataTransfer.StandardDataFormats", "UserActivityJsonArray"))
            {
                supportedDataFormats.Add(StandardDataFormats.UserActivityJsonArray);
            }
        }

        #endregion

        #region Constructors and Initialization
        /// <summary>
        /// Creates a ShareTargetOperation
        /// </summary>
        /// <param name="dataFormat">Must be one of the properties of the StandardDataFormats class or *.
        /// A dataformat with value of * means all of the data in the DataPackageView will be used</param>
        /// <param name="view">The view to be used by the dataFormat</param>
        /// <param name="priority">The priority level of the dataFormat</param>
        public ShareTargetOperation(string dataFormat, Type view, int priority = 0) : this(dataFormat, Enumerable.Empty<string>().ToList(), view, priority)
        {
        }

        public ShareTargetOperation(IEnumerable<string> fileTypes, Type view, int priority = 0) : this(StandardDataFormats.StorageItems, fileTypes, view, priority)
        {
        }

        private ShareTargetOperation(string dataFormat, IEnumerable<string> fileTypes, Type view, int priority)
        {
            if (!supportedDataFormats.Contains(dataFormat) && dataFormat != "*")
            {
                throw new ArgumentException("Data Format is not supported");
            }

            DataFormat = dataFormat;
            FileTypes = new ReadOnlyCollection<string>(fileTypes.ToList());
            View = view ?? throw new ArgumentNullException(nameof(view), "View cannot be null");
            Priority = priority;
        }

        public string DataFormat { get; }

        /// <summary>
        /// If Data Format is StorageItems, this will contain the supported File Types. If empty, it means it supports any file type
        /// </summary>
        public ReadOnlyCollection<string> FileTypes { get; }

        public Type View { get; }

        public int Priority { get; }

        #endregion

        public async Task Run(ShareTargetActivatedEventArgs args)
        {
            if (Window.Current.Content is Frame frame)
            {
                var payload = await Process(args.ShareOperation.Data);
                frame.Navigate(View, payload);
            }
        }

        protected virtual async Task<SharePayload> Process(DataPackageView data)
        {
            if(DataFormat != "*")
            {
                return await data.GetPayload(DataFormat);
            }
            else
            {
                return await data.GetPayload();
            }
        }
    }
}
