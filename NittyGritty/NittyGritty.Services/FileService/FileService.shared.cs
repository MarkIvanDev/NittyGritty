using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Storage;

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

    }
}
