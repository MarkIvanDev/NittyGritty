using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.Platform.Files
{
    public interface IFile : IStorageItem
    {
        string FileType { get; }

        Task<Stream> GetStream(bool canWrite);
    }
}
