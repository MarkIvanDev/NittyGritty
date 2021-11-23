using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Launcher;
using NittyGritty.Platform.Storage;

namespace NittyGritty.Services.Core
{
    public interface ILauncherService
    {
        Task<bool> LaunchFile(IFile file);

        Task<bool> LaunchFileWith(IFile file);

        Task<bool> LaunchFolder(IFolder folder);

        Task<bool> LaunchFolder(IFolder folder, IEnumerable<IStorageItem> itemsToSelect);

        Task<bool> LaunchUri(Uri uri);

        Task<bool> QueryFileSupport(IFile file);

        Task<bool> QueryUriSupport(Uri uri, LaunchType launchType);

        Task<bool> QueryAppUriSupport(Uri uri);

        Task<ReadOnlyCollection<IAppInfo>> FindFileHandlers(string extension);

        Task<ReadOnlyCollection<IAppInfo>> FindUriSchemeHandlers(string scheme);

        Task<ReadOnlyCollection<IAppInfo>> FindUriSchemeHandlers(string scheme, LaunchType launchType);

        Task<ReadOnlyCollection<IAppInfo>> FindAppUriHandlers(Uri uri);
    }
}
