using NittyGritty.Platform.Files;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace NittyGritty.Uwp.Platform
{
    public abstract class FileProcessor<T> : IFileProcessor
    {
        /// <summary>
        /// Creates a file processor
        /// </summary>
        /// <param name="fileType">A file type with a value of * will be used as fallback for unknown file types</param>
        public FileProcessor(string fileType)
        {
            FileType = fileType;
        }

        public string FileType { get; }

        public abstract Task<T> Process(IStorageFile file);

        async Task<object> IFileProcessor.Process(object file)
        {
            return await Process(file as IStorageFile);
        }
    }
}
