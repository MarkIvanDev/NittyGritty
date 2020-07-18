using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Launcher;
using NittyGritty.Platform.Storage;

namespace NittyGritty.Services
{
    public partial class LauncherService
    {
        Task<ReadOnlyCollection<IAppInfo>> PlatformFindAppUriHandlers(Uri uri)
            => throw new NotImplementedException();

        Task<ReadOnlyCollection<IAppInfo>> PlatformFindFileHandlers(string extension)
            => throw new NotImplementedException();

        Task<ReadOnlyCollection<IAppInfo>> PlatformFindUriSchemeHandlers(string scheme)
            => throw new NotImplementedException();

        Task<ReadOnlyCollection<IAppInfo>> PlatformFindUriSchemeHandlers(string scheme, LaunchType launchType)
            => throw new NotImplementedException();

        Task<bool> PlatformLaunchFile(IFile file)
            => throw new NotImplementedException();

        Task<bool> PlatformLaunchFileWith(IFile file)
            => throw new NotImplementedException();

        Task<bool> PlatformLaunchFolder(IFolder folder)
            => throw new NotImplementedException();

        Task<bool> PlatformLaunchFolder(IFolder folder, IEnumerable<IStorageItem> itemsToSelect)
            => throw new NotImplementedException();

        Task<bool> PlatformLaunchUri(Uri uri)
            => throw new NotImplementedException();

        Task<bool> PlatformQueryAppUriSupport(Uri uri)
            => throw new NotImplementedException();

        Task<bool> PlatformQueryFileSupport(IFile file)
            => throw new NotImplementedException();

        Task<bool> PlatformQueryUriSupport(Uri uri, LaunchType launchType)
            => throw new NotImplementedException();
    }
}
