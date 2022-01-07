using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Storage;
using NittyGritty.Services.Core;
using NGIStorageItem = NittyGritty.Platform.Storage.IStorageItem;
using Windows.Storage;
using Windows.System;

namespace NittyGritty.Uwp.Services
{
    public class FileService : IFileService
    {
        public async Task RequestAccess()
        {
            await Launcher.LaunchUriAsync(new Uri("ms-settings:privacy-broadfilesystemaccess"));
        }

        public async Task<bool> Launch(IFile file)
        {
            if (file is NGFile ngFile)
            {
                return await Launcher.LaunchFileAsync(ngFile.Context);
            }
            return false;
        }

        public async Task<bool> Launch(IFolder folder)
        {
            if (folder is NGFolder ngFolder)
            {
                return await Launcher.LaunchFolderAsync(ngFolder.Context);
            }
            return false;
        }

        public async Task<IFile> GetFileFromPath(string path)
        {
            var file = await StorageFile.GetFileFromPathAsync(path);
            return new NGFile(file);
        }

        public async Task<IFolder> GetFolderFromPath(string path)
        {
            var folder = await StorageFolder.GetFolderFromPathAsync(path);
            return new NGFolder(folder);
        }

        public async Task<IFile> CreateFile(IFolder destination, string name, CreationOption option)
        {
            if (destination is NGFolder folder)
            {
                var f = await folder.Context.CreateFileAsync(name, (CreationCollisionOption)option);
                return new NGFile(f);
            }
            return null;
        }

        public async Task<IFolder> CreateFolder(IFolder destination, string name, CreationOption option)
        {
            if (destination is NGFolder folder)
            {
                var f = await folder.Context.CreateFolderAsync(name, (CreationCollisionOption)option);
                return new NGFolder(f);
            }
            return null;
        }

        public async Task<IFile> Copy(IFile file, IFolder destination, string newName, RenameOption option)
        {
            if (file is NGFile ngfile && destination is NGFolder ngfolder)
            {
                var f = await ngfile.Context.CopyAsync(ngfolder.Context, newName, (NameCollisionOption)option);
                return new NGFile(f);
            }
            return null;
        }

        public  async Task Move(IFile file, IFolder destination, string newName, RenameOption option)
        {
            if (file is NGFile ngfile && destination is NGFolder ngfolder)
            {
                await ngfile.Context.MoveAsync(ngfolder.Context, newName, (NameCollisionOption)option);
            }
        }

        public async Task Rename(NGIStorageItem item, string newName, RenameOption option)
        {
            if (item.Context is Windows.Storage.IStorageItem winItem)
            {
                await winItem.RenameAsync(newName, (NameCollisionOption)option);
            }
        }

        public async Task Delete(NGIStorageItem item, bool deletePermanently)
        {
            if (item.Context is Windows.Storage.IStorageItem winItem)
            {
                await winItem.DeleteAsync(deletePermanently ? StorageDeleteOption.PermanentDelete : StorageDeleteOption.Default);
            }
        }
    }
}
