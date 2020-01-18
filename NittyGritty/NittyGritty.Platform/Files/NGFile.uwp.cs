using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace NittyGritty.Platform.Files
{
    public class NGFile : IFile
    {
        private readonly IStorageFile file;

        public NGFile(IStorageFile file)
        {
            this.file = file;
        }

        public string Name { get { return file.Name; } }

        public string FileType { get { return file.FileType; } }

        public string Path { get { return file.Path; } }

        public string DisplayName { get { return (file as IStorageItemProperties)?.DisplayName; } }

        public string DisplayType { get { return (file as IStorageItemProperties)?.DisplayType; } }

        public StorageItemType Type { get; } = StorageItemType.File;

        public async Task<Stream> GetStream(bool canWrite)
        {
            var content = canWrite ? await file.OpenAsync(FileAccessMode.ReadWrite) : await file.OpenAsync(FileAccessMode.Read);            return canWrite ? content.AsStreamForWrite() : content.AsStreamForRead();
        }
    }
}
