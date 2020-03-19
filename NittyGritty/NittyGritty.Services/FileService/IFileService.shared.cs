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

        Task<IFile> CreateFile(IFolder destination, string name, CreationCollisionOption option);

        Task<IFolder> CreateFolder(IFolder destination, string name, CreationCollisionOption option);

        Task<IFile> Copy(IFile file, IFolder destination, string newName, NameCollisionOption option);

        Task Move(IFile file, IFolder destination, string newName, NameCollisionOption option);

        Task Rename(IStorageItem item, string newName, NameCollisionOption option);

        Task Delete(IStorageItem item, bool deletePermanently);
    }
}
