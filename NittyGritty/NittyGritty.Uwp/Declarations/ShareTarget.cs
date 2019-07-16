using NittyGritty.Utilities;
using NittyGritty.Views;
using NittyGritty.Views.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.DataTransfer.ShareTarget;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp.Declarations
{
    public abstract class ShareTarget
    {

        private ShareOperation shareOperation;


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

        static ShareTarget()
        {
            if (ApiInformation.IsReadOnlyPropertyPresent("Windows.ApplicationModel.DataTransfer.StandardDataFormats", "UserActivityJsonArray"))
            {
                supportedDataFormats.Add(StandardDataFormats.UserActivityJsonArray);
            }
        }

        #endregion

        #region Constructors and Initialization

        public ShareTarget(string dataFormat, int priority = 0, Type view = null) : this(dataFormat, Enumerable.Empty<string>().ToList(), priority, view)
        {
        }

        public ShareTarget(IEnumerable<string> fileTypes, int priority = 0, Type view = null) : this(StandardDataFormats.StorageItems, fileTypes, priority, view)
        {
        }

        private ShareTarget(string dataFormat, IEnumerable<string> fileTypes, int priority, Type view)
        {
            if (!supportedDataFormats.Contains(dataFormat))
            {
                throw new ArgumentException("Data Format is not supported");
            }

            DataFormat = dataFormat;
            Priority = priority;
            FileTypes = new ReadOnlyCollection<string>(fileTypes.ToList());
            View = view;
        }

        public string DataFormat { get; }

        /// <summary>
        /// If Data Format is StorageItems, this will contain the supported File Types. If empty, it means it supports any file type
        /// </summary>
        public ReadOnlyCollection<string> FileTypes { get; }

        public Type View { get; }

        public int Priority { get; }

        #endregion

        private IShareTarget _context;

        public IShareTarget Context
        {
            get { return _context; }
            private set
            {
                if(value != _context)
                {
                    DeregisterEvents();
                    _context = value;
                    RegisterEvents();
                }
            }
        }

        public async Task Run(ShareOperation shareOperation)
        {
            this.shareOperation = shareOperation;
            if (Window.Current.Content is Frame frame)
            {
                var payload = await Process(shareOperation.Data);
                frame.Navigate(View, payload);
                if(frame.Content is Page page)
                {
                    Context = page.DataContext as IShareTarget;
                }
            }
        }

        protected virtual void OnShareStarted(object sender, EventArgs e)
        {
            shareOperation.ReportStarted();
        }

        protected virtual void OnShareFailed(object sender, ShareFailedEventArgs e)
        {
            shareOperation.ReportError(e.Error);
        }

        protected virtual void OnShareCompleted(object sender, NittyGritty.Views.Events.ShareCompletedEventArgs e)
        {
            shareOperation.ReportCompleted();
        }

        protected abstract Task<SharePayload> Process(DataPackageView data);

        #region EventHandlers

        private EventHandler shareStartedEventHandler;
        private ShareFailedEventHandler shareFailedEventHandler;
        private ShareCompletedEventHandler shareCompletedEventHandler;

        private void RegisterEvents()
        {
            if (Context != null)
            {
                shareStartedEventHandler = EventUtilities.RegisterEvent<IShareTarget, EventHandler, EventArgs>(
                    Context,
                    h => Context.ShareStarted += h,
                    h => Context.ShareStarted -= h,
                    h => (o, e) => h(o, e),
                    (subscriber, s, e) => OnShareStarted(s, e));
                shareFailedEventHandler = EventUtilities.RegisterEvent<IShareTarget, ShareFailedEventHandler, ShareFailedEventArgs>(
                    Context,
                    h => Context.ShareFailed += h,
                    h => Context.ShareFailed -= h,
                    h => (o, e) => h(o, e),
                    (subscriber, s, e) => OnShareFailed(s, e));
                shareCompletedEventHandler = EventUtilities.RegisterEvent<IShareTarget, ShareCompletedEventHandler, NittyGritty.Views.Events.ShareCompletedEventArgs>(
                    Context,
                    h => Context.ShareCompleted += h,
                    h => Context.ShareCompleted -= h,
                    h => (o, e) => h(o, e),
                    (subscriber, s, e) => OnShareCompleted(s, e));
            }
        }

        private void DeregisterEvents()
        {
            if(Context != null)
            {
                if(shareStartedEventHandler != null)
                {
                    Context.ShareStarted -= shareStartedEventHandler;
                    shareStartedEventHandler = null;
                }

                if(shareFailedEventHandler != null)
                {
                    Context.ShareFailed -= shareFailedEventHandler;
                    shareFailedEventHandler = null;
                }

                if(shareCompletedEventHandler != null)
                {
                    Context.ShareCompleted -= shareCompletedEventHandler;
                    shareCompletedEventHandler = null;
                }
            }
        }

        #endregion
    }
}
