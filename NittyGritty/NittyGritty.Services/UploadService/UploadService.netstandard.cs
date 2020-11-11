using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Storage;

namespace NittyGritty.Services
{
    public partial class UploadService
    {

        Task PlatformInitialize()
            => throw new NotImplementedException();

        Task PlatformUpload(Uri uri, IFile file)
            => throw new NotImplementedException();

        Task PlatformUpload(Uri uri, IList<IFile> files)
            => throw new NotImplementedException();

        void PlatformCancel(Uri uri)
            => throw new NotImplementedException();

    }
}
