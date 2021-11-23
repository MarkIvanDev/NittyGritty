using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Storage;
using NittyGritty.Platform.Upload;
using NittyGritty.Services.Core;

namespace NittyGritty.Services
{
    public partial class UploadService : IUploadService
    {
        public event UploadProgressChangedEventHandler UploadProgressChanged;

        public Task Initialize()
            => PlatformInitialize();

        public Task Upload(Uri uri, IFile file)
            => PlatformUpload(uri, file);

        public Task Upload(Uri uri, IList<IFile> files)
            => PlatformUpload(uri, files);

        public void Cancel(Uri uri)
            => PlatformCancel(uri);

    }
}
