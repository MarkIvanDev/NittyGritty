using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Files;

namespace NittyGritty.Services
{
    public interface IPickerService
    {
        Task<IFile> Open(IList<string> fileTypes);

        Task<IFile> Save(IDictionary<string, IList<string>> fileTypes);
    }
}
