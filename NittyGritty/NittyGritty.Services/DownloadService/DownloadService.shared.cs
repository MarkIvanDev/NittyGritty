using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NittyGritty.Platform.Download;
using NittyGritty.Platform.Storage;
using NittyGritty.Services.Core;

namespace NittyGritty.Services
{
    public partial class DownloadService : IDownloadService
    {
        public event DownloadProgressChangedEventHandler DownloadProgressChanged;

        public Task Initialize()
            => PlatformInitialize();

        public Task Download(Uri uri)
            => PlatformDownload(uri);

        public Task Download(Uri uri, IFile file)
            => PlatformDownload(uri, file);

        public void Pause(Uri uri)
            => PlatformPause(uri);

        public void Resume(Uri uri)
            => PlatformResume(uri);

        public void Cancel(Uri uri)
            => PlatformCancel(uri);
    }
}
