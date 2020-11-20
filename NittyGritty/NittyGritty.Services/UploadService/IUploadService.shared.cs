using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Storage;

namespace NittyGritty.Services
{
    public interface IUploadService
    {
        Task Initialize();

        Task Upload(Uri uri, IFile file);

        Task Upload(Uri uri, IList<IFile> files);

        void Cancel(Uri uri);
    }
}
