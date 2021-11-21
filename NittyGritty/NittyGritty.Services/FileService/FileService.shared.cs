using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Storage;
using NittyGritty.Services.Core;

namespace NittyGritty.Services
{
    public partial class FileService : IFileService
    {
        public async Task RequestAccess()
        {
            await PlatformRequestAccess();
        }

        public async Task<bool> Launch(IFile file)
        {
            return await PlatformLaunch(file);
        }

        public async Task<bool> Launch(IFolder folder)
        {
            return await PlatformLaunch(folder);
        }

        public async Task<IFile> GetFileFromPath(string path)
        {
            return await PlatformGetFileFromPath(path);
        }

        public async Task<IFolder> GetFolderFromPath(string path)
        {
            return await PlatformGetFolderFromPath(path);
        }

        public async Task<IFile> CreateFile(IFolder destination, string name, CreationCollisionOption option)
        {
            return await PlatformCreateFile(destination, name, option);
        }

        public async Task<IFolder> CreateFolder(IFolder destination, string name, CreationCollisionOption option)
        {
            return await PlatformCreateFolder(destination, name, option);
        }

        public async Task<IFile> Copy(IFile file, IFolder destination, string newName, NameCollisionOption option)
        {
            return await PlatformCopy(file, destination, newName, option);
        }

        public async Task Move(IFile file, IFolder destination, string newName, NameCollisionOption option)
        {
            await PlatformMove(file, destination, newName, option);
        }

        public async Task Rename(IStorageItem item, string newName, NameCollisionOption option)
        {
            await PlatformRename(item, newName, option);
        }

        public async Task Delete(IStorageItem item, bool deletePermanently)
        {
            await PlatformDelete(item, deletePermanently);
        }
    }
}
