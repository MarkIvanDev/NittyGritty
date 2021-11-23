using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NittyGritty.Platform.Storage;

namespace NittyGritty.Services.Core
{
    public interface IDownloadService
    {
        Task Initialize();

        Task Download(Uri uri);

        Task Download(Uri uri, IFile file);

        void Pause(Uri uri);

        void Resume(Uri uri);

        void Cancel(Uri uri);
    }
}
