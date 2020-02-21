using NittyGritty.Platform.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.Platform.Data
{
    public interface IFilePayload
    {
        string Action { get; }

        IReadOnlyCollection<string> AvailableFileTypes { get; }

        Task<IReadOnlyCollection<IFile>> GetFiles();

        Task<IFile> GetFile(string fileName);
    }
}
