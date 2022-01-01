using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Storage;

namespace NittyGritty.Services.Core
{
    public interface IPickerService
    {
        Task<IFile> OpenFile(IList<string> fileTypes);

        Task<IList<IFile>> OpenFiles(IList<string> fileTypes);

        Task<IFile> SaveFile(IDictionary<string, IList<string>> fileTypes);

        Task<IFolder> OpenFolder();
    }
}
