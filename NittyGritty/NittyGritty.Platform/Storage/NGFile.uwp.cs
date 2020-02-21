using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace NittyGritty.Platform.Storage
{
    public class NGFile : IFile
    {
        public NGFile(IStorageFile file)
        {
            Context = file;
        }

        object IStorageItem.Context { get { return Context; } }

        public IStorageFile Context { get; }

        public string Name { get { return Context.Name; } }

        public string FileType { get { return Context.FileType; } }

        public string Path { get { return Context.Path; } }

        public string DisplayName { get { return (Context as IStorageItemProperties)?.DisplayName; } }

        public string DisplayType { get { return (Context as IStorageItemProperties)?.DisplayType; } }

        public StorageItemType Type { get; } = StorageItemType.File;

        public async Task<Stream> GetStream(bool canWrite)
        {
            var content = canWrite ? await Context.OpenAsync(FileAccessMode.ReadWrite) : await Context.OpenAsync(FileAccessMode.Read);            return canWrite ? content.AsStreamForWrite() : content.AsStreamForRead();
        }
    }
}
