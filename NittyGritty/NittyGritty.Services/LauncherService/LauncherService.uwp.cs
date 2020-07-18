using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Launcher;
using NittyGritty.Platform.Storage;
using Windows.Storage;
using Windows.System;

namespace NittyGritty.Services
{
    public partial class LauncherService
    {
        async Task<ReadOnlyCollection<IAppInfo>> PlatformFindAppUriHandlers(Uri uri)
        {
            var appInfos = new List<IAppInfo>();
            var handlers = await Launcher.FindAppUriHandlersAsync(uri);
            foreach (var item in handlers)
            {
                appInfos.Add(new NGAppInfo(item));
            }
            return new ReadOnlyCollection<IAppInfo>(appInfos);
        }

        async Task<ReadOnlyCollection<IAppInfo>> PlatformFindFileHandlers(string extension)
        {
            var appInfos = new List<IAppInfo>();
            var handlers = await Launcher.FindFileHandlersAsync(extension);
            foreach (var item in handlers)
            {
                appInfos.Add(new NGAppInfo(item));
            }
            return new ReadOnlyCollection<IAppInfo>(appInfos);
        }

        async Task<ReadOnlyCollection<IAppInfo>> PlatformFindUriSchemeHandlers(string scheme)
        {
            var appInfos = new List<IAppInfo>();
            var handlers = await Launcher.FindUriSchemeHandlersAsync(scheme);
            foreach (var item in handlers)
            {
                appInfos.Add(new NGAppInfo(item));
            }
            return new ReadOnlyCollection<IAppInfo>(appInfos);
        }

        async Task<ReadOnlyCollection<IAppInfo>> PlatformFindUriSchemeHandlers(string scheme, LaunchType launchType)
        {
            var appInfos = new List<IAppInfo>();
            var handlers = await Launcher.FindUriSchemeHandlersAsync(scheme, (LaunchQuerySupportType)launchType);
            foreach (var item in handlers)
            {
                appInfos.Add(new NGAppInfo(item));
            }
            return new ReadOnlyCollection<IAppInfo>(appInfos);
        }

        async Task<bool> PlatformLaunchFile(IFile file)
        {
            return await Launcher.LaunchFileAsync(file.Context as IStorageFile);
        }

        async Task<bool> PlatformLaunchFileWith(IFile file)
        {
            return await Launcher.LaunchFileAsync(file.Context as IStorageFile, new LauncherOptions { DisplayApplicationPicker = true });
        }

        async Task<bool> PlatformLaunchFolder(IFolder folder)
        {
            return await Launcher.LaunchFolderAsync(folder.Context as IStorageFolder);
        }

        async Task<bool> PlatformLaunchFolder(IFolder folder, IEnumerable<Platform.Storage.IStorageItem> itemsToSelect)
        {
            var options = new FolderLauncherOptions();
            foreach (var item in itemsToSelect)
            {
                options.ItemsToSelect.Add(item.Context as Windows.Storage.IStorageItem);
            }
            return await Launcher.LaunchFolderAsync(folder.Context as IStorageFolder, options);
        }

        async Task<bool> PlatformLaunchUri(Uri uri)
        {
            return await Launcher.LaunchUriAsync(uri);
        }

        async Task<bool> PlatformQueryAppUriSupport(Uri uri)
        {
            var result = await Launcher.QueryAppUriSupportAsync(uri);
            return result == LaunchQuerySupportStatus.Available;
        }

        async Task<bool> PlatformQueryFileSupport(IFile file)
        {
            var result = await Launcher.QueryFileSupportAsync(file.Context as StorageFile);
            return result == LaunchQuerySupportStatus.Available;
        }

        async Task<bool> PlatformQueryUriSupport(Uri uri, LaunchType launchType)
        {
            var result = await Launcher.QueryUriSupportAsync(uri, (LaunchQuerySupportType)launchType);
            return result == LaunchQuerySupportStatus.Available;
        }
    }
}
