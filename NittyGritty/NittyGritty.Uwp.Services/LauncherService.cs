using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Launcher;
using NittyGritty.Platform.Storage;
using NittyGritty.Services.Core;
using Windows.Storage;
using Windows.System;

namespace NittyGritty.Uwp.Services
{
    public class LauncherService : ILauncherService
    {
        public async Task<IList<IAppInfo>> FindAppUriHandlers(Uri uri)
        {
            var appInfos = new List<IAppInfo>();
            var handlers = await Launcher.FindAppUriHandlersAsync(uri);
            foreach (var item in handlers)
            {
                appInfos.Add(new NGAppInfo(item));
            }
            return appInfos;
        }

        public async Task<IList<IAppInfo>> FindFileHandlers(string extension)
        {
            var appInfos = new List<IAppInfo>();
            var handlers = await Launcher.FindFileHandlersAsync(extension);
            foreach (var item in handlers)
            {
                appInfos.Add(new NGAppInfo(item));
            }
            return appInfos;
        }

        public async Task<IList<IAppInfo>> FindUriSchemeHandlers(string scheme)
        {
            var appInfos = new List<IAppInfo>();
            var handlers = await Launcher.FindUriSchemeHandlersAsync(scheme);
            foreach (var item in handlers)
            {
                appInfos.Add(new NGAppInfo(item));
            }
            return appInfos;
        }

        public async Task<IList<IAppInfo>> FindUriSchemeHandlers(string scheme, LaunchType launchType)
        {
            var appInfos = new List<IAppInfo>();
            var handlers = await Launcher.FindUriSchemeHandlersAsync(scheme, (LaunchQuerySupportType)launchType);
            foreach (var item in handlers)
            {
                appInfos.Add(new NGAppInfo(item));
            }
            return appInfos;
        }

        public async Task<bool> LaunchFile(IFile file)
        {
            return await Launcher.LaunchFileAsync(file.Context as IStorageFile);
        }

        public async Task<bool> LaunchFileWith(IFile file)
        {
            return await Launcher.LaunchFileAsync(file.Context as IStorageFile, new LauncherOptions { DisplayApplicationPicker = true });
        }

        public async Task<bool> LaunchFolder(IFolder folder)
        {
            return await Launcher.LaunchFolderAsync(folder.Context as IStorageFolder);
        }

        public async Task<bool> LaunchFolder(IFolder folder, IEnumerable<Platform.Storage.IStorageItem> itemsToSelect)
        {
            var options = new FolderLauncherOptions();
            foreach (var item in itemsToSelect)
            {
                options.ItemsToSelect.Add(item.Context as Windows.Storage.IStorageItem);
            }
            return await Launcher.LaunchFolderAsync(folder.Context as IStorageFolder, options);
        }

        public async Task<bool> LaunchUri(Uri uri)
        {
            return await Launcher.LaunchUriAsync(uri);
        }

        public async Task<bool> QueryAppUriSupport(Uri uri)
        {
            var result = await Launcher.QueryAppUriSupportAsync(uri);
            return result == LaunchQuerySupportStatus.Available;
        }

        public async Task<bool> QueryFileSupport(IFile file)
        {
            var result = await Launcher.QueryFileSupportAsync(file.Context as StorageFile);
            return result == LaunchQuerySupportStatus.Available;
        }

        public async Task<bool> QueryUriSupport(Uri uri, LaunchType launchType)
        {
            var result = await Launcher.QueryUriSupportAsync(uri, (LaunchQuerySupportType)launchType);
            return result == LaunchQuerySupportStatus.Available;
        }
    }
}
