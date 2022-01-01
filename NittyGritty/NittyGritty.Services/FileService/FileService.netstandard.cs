using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Storage;

namespace NittyGritty.Services
{
    public partial class FileService
    {
        Task PlatformRequestAccess()
        {
            throw new NotImplementedException();
        }

        Task<bool> PlatformLaunch(IFile file)
        {
            throw new NotImplementedException();
        }

        Task<bool> PlatformLaunch(IFolder folder)
        {
            throw new NotImplementedException();
        }

        Task<IFile> PlatformGetFileFromPath(string path)
        {
            throw new NotImplementedException();
        }

        Task<IFolder> PlatformGetFolderFromPath(string path)
        {
            throw new NotImplementedException();
        }

        Task<IFile> PlatformCreateFile(IFolder destination, string name, CreationOption option)
        {
            throw new NotImplementedException();
        }

        Task<IFolder> PlatformCreateFolder(IFolder destination, string name, CreationOption option)
        {
            throw new NotImplementedException();
        }

        Task<IFile> PlatformCopy(IFile file, IFolder destination, string newName, RenameOption option)
        {
            throw new NotImplementedException();
        }

        Task PlatformMove(IFile file, IFolder destination, string newName, RenameOption option)
        {
            throw new NotImplementedException();
        }

        Task PlatformRename(IStorageItem item, string newName, RenameOption option)
        {
            throw new NotImplementedException();
        }

        Task PlatformDelete(IStorageItem item, bool deletePermanently)
        {
            throw new NotImplementedException();
        }
    }
}
