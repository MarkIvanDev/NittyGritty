using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Files;

namespace NittyGritty.Services
{
    public partial class PickerService : IPickerService
    {
        public async Task<IFile> Open(IList<string> fileTypes)
        {
            return await PlatformOpen(fileTypes);
        }

        public async Task<IFile> Save(IDictionary<string, IList<string>> fileTypes)
        {
            return await PlatformSave(fileTypes);
        }

        public Task Save(IDictionary<string, IList<string>> fileTypes, Func<Stream, Task> writer)
        {
            throw new NotImplementedException();
        }
    }
}
