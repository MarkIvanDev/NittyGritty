using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Files;

namespace NittyGritty.Services
{
    public partial class PickerService
    {
        Task<IFile> PlatformOpen(IList<string> fileTypes)
            => throw new NotImplementedException();

        Task<IFile> PlatformSave(IDictionary<string, IList<string>> fileTypes)
            => throw new NotImplementedException();

    }
}
