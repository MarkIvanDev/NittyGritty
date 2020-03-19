using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Storage;
using Windows.Storage;
using Windows.System;
using NGIStorageItem = NittyGritty.Platform.Storage.IStorageItem;
using NGNameCollisionOption = NittyGritty.Platform.Storage.NameCollisionOption;
using NGCreationCollisionOption = NittyGritty.Platform.Storage.CreationCollisionOption;

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

        async Task<IFile> PlatformCreateFile(IFolder destination, string name, NGCreationCollisionOption option)
        {
            if(destination is NGFolder folder)
            {
                var f = await folder.Context.CreateFileAsync(name, (Windows.Storage.CreationCollisionOption)option);
                return new NGFile(f);
            }
            return null;
        }

        async Task<IFolder> PlatformCreateFolder(IFolder destination, string name, NGCreationCollisionOption option)
        {
            if (destination is NGFolder folder)
            {
                var f = await folder.Context.CreateFolderAsync(name, (Windows.Storage.CreationCollisionOption)option);
                return new NGFolder(f);
            }
            return null;
        }

        async Task<IFile> PlatformCopy(IFile file, IFolder destination, string newName, NGNameCollisionOption option)
        {
            if(file is NGFile ngfile && destination is NGFolder ngfolder)
            {
                var f = await ngfile.Context.CopyAsync(ngfolder.Context, newName, (Windows.Storage.NameCollisionOption)option);
                return new NGFile(f);
            }
            return null;
        }

        async Task PlatformMove(IFile file, IFolder destination, string newName, NGNameCollisionOption option)
        {
            if (file is NGFile ngfile && destination is NGFolder ngfolder)
            {
                await ngfile.Context.MoveAsync(ngfolder.Context, newName, (Windows.Storage.NameCollisionOption)option);
            }
        }

        async Task PlatformRename(NGIStorageItem item, string newName, NGNameCollisionOption option)
        {
            if(item.Context is Windows.Storage.IStorageItem winItem)
            {
                await winItem.RenameAsync(newName, (Windows.Storage.NameCollisionOption)option);
            }
        }

        async Task PlatformDelete(NGIStorageItem item, bool deletePermanently)
        {
            if(item.Context is Windows.Storage.IStorageItem winItem)
            {
                await winItem.DeleteAsync(deletePermanently ? StorageDeleteOption.PermanentDelete : StorageDeleteOption.Default);
            }
        }
    }
}
