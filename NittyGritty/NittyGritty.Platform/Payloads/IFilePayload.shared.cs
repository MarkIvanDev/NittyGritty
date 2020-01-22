using NittyGritty.Platform.Files;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.Platform.Payloads
{
    public interface IFilePayload
    {
        string Action { get; }

        IReadOnlyCollection<string> AvailableFileTypes { get; }

        Task<IReadOnlyCollection<IFile>> GetFiles(bool canWrite);

        Task<IFile> GetFile(string fileName, bool canWrite);
    }
}
