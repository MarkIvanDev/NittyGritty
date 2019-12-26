using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Files;

namespace NittyGritty.Platform.Services
{
    public interface IPickerService
    {
        Task<NGFile> Open(IList<string> fileTypes);

        Task<NGFile> Save(IDictionary<string, IList<string>> fileTypes);

        Task SaveAndOpen(IDictionary<string, IList<string>> fileTypes, Func<Stream, Task> writer);
    }
}
