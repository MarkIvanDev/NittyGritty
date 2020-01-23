using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Storage;

namespace NittyGritty.Services
{
    public partial class FileService
    {
        Task PlatformRequestAccess()
        {
            throw new NotImplementedException();
        }

        Task<bool> PlatformLaunch(IFile file)
        {
            throw new NotImplementedException();
        }

        Task<bool> PlatformLaunch(IFolder folder)
        {
            throw new NotImplementedException();
        }
    }
}
