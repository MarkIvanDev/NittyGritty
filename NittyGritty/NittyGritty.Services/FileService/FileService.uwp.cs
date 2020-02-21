using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Storage;
using Windows.Storage;
using Windows.System;

namespace NittyGritty.Services
{
    public partial class FileService
    {
        async Task PlatformRequestAccess()
        {
            await Launcher.LaunchUriAsync(new Uri("ms-settings:privacy-broadfilesystemaccess"));
        }

        async Task<bool> PlatformLaunch(IFile file)
        {
            if(file is NGFile ngFile)
            {
                return await Launcher.LaunchFileAsync(ngFile.Context);
            }
            return false;
        }

        async Task<bool> PlatformLaunch(IFolder folder)
        {
            if(folder is NGFolder ngFolder)
            {
                return await Launcher.LaunchFolderAsync(ngFolder.Context);
            }
            return false;
        }

        async Task<IFile> PlatformGetFileFromPath(string path)
        {
            var file = await StorageFile.GetFileFromPathAsync(path);
            return new NGFile(file);
        }

        async Task<IFolder> PlatformGetFolderFromPath(string path)
        {
            var folder = await StorageFolder.GetFolderFromPathAsync(path);
            return new NGFolder(folder);
        }
    }
}
