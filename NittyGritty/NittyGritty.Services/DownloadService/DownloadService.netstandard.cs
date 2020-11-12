using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Storage;

namespace NittyGritty.Services
{
    public partial class DownloadService
    {
        Task PlatformInitialize()
            => throw new NotImplementedException();

        Task PlatformDownload(Uri uri)
            => throw new NotImplementedException();

        Task PlatformDownload(Uri uri, IFile file)
            => throw new NotImplementedException();

        void PlatformPause(Uri uri)
            => throw new NotImplementedException();

        void PlatformResume(Uri uri)
            => throw new NotImplementedException();

        void PlatformCancel(Uri uri)
            => throw new NotImplementedException();
    }
}
