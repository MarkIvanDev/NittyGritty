using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Data;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace NittyGritty.Services
{
    public partial class ClipboardService
    {
        private bool isWindowActive = false;
        private bool isClipboardContentChangedPending = false;

        void PlatformStart()
        {
            Window.Current.Activated += OnWindowActivated;
            Clipboard.ContentChanged += OnClipboardContentChanged;
        }

        void PlatformStop()
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
                    if(isClipboardContentChangedPending)
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
            if(isWindowActive)
            {
                isClipboardContentChangedPending = false;
                ContentChanged?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                isClipboardContentChangedPending = true;
            }
        }

        void PlatformCopy(DataPayload data)
        {
            var package = data.AsDataPackage();
            package.RequestedOperation = DataPackageOperation.Copy;
            Clipboard.SetContent(package);
            Clipboard.Flush();
        }

        void PlatformCut(DataPayload data)
        {
            var package = data.AsDataPackage();
            package.RequestedOperation = DataPackageOperation.Move;
            Clipboard.SetContent(package);
            Clipboard.Flush();
        }

        async Task<DataPayload> PlatformGetData()
        {
            var dataPackage = Clipboard.GetContent();
            return await dataPackage.GetData();
        }

        void PlatformClear()
        {
            Clipboard.Clear();
        }
    }
}
