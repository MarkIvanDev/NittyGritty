using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.Platform.Files
{
    public interface IFile
    {
        string Name { get; }

        string FileType { get; }

        string Path { get; }

        Task<Stream> GetStream(bool canWrite);
    }
}
