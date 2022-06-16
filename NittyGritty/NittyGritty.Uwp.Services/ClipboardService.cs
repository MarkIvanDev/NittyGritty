using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Data;
using NittyGritty.Services.Core;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace NittyGritty.Uwp.Services
{
    public class ClipboardService : IClipboardService
    {
        public event EventHandler ContentChanged;

        private bool isWindowActive = false;
        private bool isClipboardContentChangedPending = false;

        public void Start()
        {
            Window.Current.Activated += OnWindowActivated;
            Clipboard.ContentChanged += OnClipboardContentChanged;
        }

        public void Stop()
        {
            Window.Current.Activated -= OnWindowActivated;
            Clipboard.ContentChanged -= OnClipboardContentChanged;
        }

        private void OnWindowActivated(object sender, WindowActivatedEventArgs e)
        {
            switch (e.WindowActivationState)
            {
                case CoreWindowActivationState.CodeActivated:
                case CoreWindowActivationState.PointerActivated:
                    isWindowActive = true;
                    if (isClipboardContentChangedPending)
                    {
                        isClipboardContentChangedPending = false;
                        ContentChanged?.Invoke(this, EventArgs.Empty);
                    }
                    break;
                case CoreWindowActivationState.Deactivated:
                default:
                    isWindowActive = false;
                    break;
            }
        }

        private void OnClipboardContentChanged(object sender, object e)
        {
            if (isWindowActive)
            {
                isClipboardContentChangedPending = false;
                ContentChanged?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                isClipboardContentChangedPending = true;
            }
        }

        public void Copy(DataPayload data)
        {
            var package = data.AsDataPackage();
            package.RequestedOperation = DataPackageOperation.Copy;
            Clipboard.SetContent(package);
            Clipboard.Flush();
        }

        public void Cut(DataPayload data)
        {
            var package = data.AsDataPackage();
            package.RequestedOperation = DataPackageOperation.Move;
            Clipboard.SetContent(package);
            Clipboard.Flush();
        }

        public async Task<DataPayload> GetData()
        {
            var dataPackage = Clipboard.GetContent();
            return await dataPackage.GetData();
        }

        public bool ContainsText()
        {
            return ContainsData(StandardDataFormats.Text);
        }

        public bool ContainsBitmap()
        {
            return ContainsData(StandardDataFormats.Bitmap);
        }

        public bool ContainsHtml()
        {
            return ContainsData(StandardDataFormats.Html);
        }

        public bool ContainsRtf()
        {
            return ContainsData(StandardDataFormats.Rtf);
        }

        public bool ContainsStorageItems()
        {
            return ContainsData(StandardDataFormats.StorageItems);
        }

        public bool ContainsAppLink()
        {
            return ContainsData(StandardDataFormats.ApplicationLink);
        }

        public bool ContainsWebLink()
        {
            return ContainsData(StandardDataFormats.WebLink);
        }

        public bool ContainsData(string formatId)
        {
            return Clipboard.GetContent().Contains(formatId);
        }

        public void Clear()
        {
            Clipboard.Clear();
        }
    }
}
