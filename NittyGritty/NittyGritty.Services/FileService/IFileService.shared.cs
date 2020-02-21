using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Storage;

namespace NittyGritty.Services
{
    public interface IFileService
    {
        Task RequestAccess();

        Task<bool> Launch(IFile file);

        Task<bool> Launch(IFolder folder);

        Task<IFile> GetFileFromPath(string path);

        Task<IFolder> GetFolderFromPath(string path);
    }
}
