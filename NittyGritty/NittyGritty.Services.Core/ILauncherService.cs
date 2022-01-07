using System;
using System.Collections.Generic;
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

        Task<IList<IAppInfo>> FindFileHandlers(string extension);

        Task<IList<IAppInfo>> FindUriSchemeHandlers(string scheme);

        Task<IList<IAppInfo>> FindUriSchemeHandlers(string scheme, LaunchType launchType);

        Task<IList<IAppInfo>> FindAppUriHandlers(Uri uri);
    }
}
