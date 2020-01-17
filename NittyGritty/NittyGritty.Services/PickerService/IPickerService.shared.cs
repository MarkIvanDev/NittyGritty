using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Services
{
    public interface IPickerService
    {
        Task<NGFile> Open(IList<string> fileTypes);

        Task<NGFile> Save(IDictionary<string, IList<string>> fileTypes);

        Task SaveAndOpen(IDictionary<string, IList<string>> fileTypes, Func<Stream, Task> writer);
    }
}
