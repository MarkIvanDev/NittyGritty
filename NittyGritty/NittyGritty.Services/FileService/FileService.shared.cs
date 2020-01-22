using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Files;

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

    }
}
