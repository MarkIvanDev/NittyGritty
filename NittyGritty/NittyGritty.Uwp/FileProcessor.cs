using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace NittyGritty.Uwp
{
    public class FileProcessor
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

        public virtual async Task<object> Process(IStorageFile file)
        {
            var stream = await file.OpenReadAsync();
            return stream.AsStream();
        }

    }
}
