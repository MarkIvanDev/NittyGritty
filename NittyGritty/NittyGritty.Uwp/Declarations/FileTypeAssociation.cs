using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;

namespace NittyGritty.Uwp.Declarations
{
    public abstract class FileTypeAssociation
    {
        public FileTypeAssociation(string fileType)
        {
            FileType = fileType;
        }

        public string FileType { get; }

        private Dictionary<string, IStorageFile> files = new Dictionary<string, IStorageFile>();

        public IStorageFile Add(IStorageFile file)
        {
            if(file.FileType.Equals(FileType))
            {
                files.TryAdd(file.Path, file);
            }
            return Get(file.Path);
        }

        public IStorageFile Get(string path)
        {
            if(files.TryGetValue(path, out var file))
            {
                return file;
            }
            return null;
        }

        public void Remove(string path)
        {
            files.Remove(path);
        }

        public async Task Run(IStorageFile file)
        {
            var storageFile = Add(file);

            await Process(storageFile);
        }

        protected abstract Task Process(IStorageFile storageFile);
    }
}
