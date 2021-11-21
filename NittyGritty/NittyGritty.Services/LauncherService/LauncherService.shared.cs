using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Launcher;
using NittyGritty.Platform.Storage;
using NittyGritty.Services.Core;

namespace NittyGritty.Services
{
    public partial class LauncherService : ILauncherService
    {
        public async Task<ReadOnlyCollection<IAppInfo>> FindAppUriHandlers(Uri uri)
            => await PlatformFindAppUriHandlers(uri);

        public async Task<ReadOnlyCollection<IAppInfo>> FindFileHandlers(string extension)
            => await PlatformFindFileHandlers(extension);

        public async Task<ReadOnlyCollection<IAppInfo>> FindUriSchemeHandlers(string scheme)
            => await PlatformFindUriSchemeHandlers(scheme);

        public async Task<ReadOnlyCollection<IAppInfo>> FindUriSchemeHandlers(string scheme, LaunchType launchType)
            => await PlatformFindUriSchemeHandlers(scheme, launchType);

        public async Task<bool> LaunchFile(IFile file)
            => await PlatformLaunchFile(file);

        public async Task<bool> LaunchFileWith(IFile file)
            => await PlatformLaunchFileWith(file);

        public async Task<bool> LaunchFolder(IFolder folder)
            => await PlatformLaunchFolder(folder);

        public async Task<bool> LaunchFolder(IFolder folder, IEnumerable<IStorageItem> itemsToSelect)
            => await PlatformLaunchFolder(folder, itemsToSelect);

        public async Task<bool> LaunchUri(Uri uri)
            => await PlatformLaunchUri(uri);

        public async Task<bool> QueryAppUriSupport(Uri uri)
            => await PlatformQueryAppUriSupport(uri);

        public async Task<bool> QueryFileSupport(IFile file)
            => await PlatformQueryFileSupport(file);

        public async Task<bool> QueryUriSupport(Uri uri, LaunchType launchType)
            => await PlatformQueryUriSupport(uri, launchType);
    }
}
