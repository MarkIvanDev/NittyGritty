using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Data;
using Windows.ApplicationModel.DataTransfer;

namespace NittyGritty.Services
{
    public partial class ClipboardService
    {

        void PlatformStart()
        {
            Clipboard.ContentChanged += OnClipboardContentChanged;
        }

        void PlatformStop()
        {
            Clipboard.ContentChanged -= OnClipboardContentChanged;
        }

        private void OnClipboardContentChanged(object sender, object e)
        {
            ContentChanged?.Invoke(this, EventArgs.Empty);
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
