using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace NittyGritty.Platform.Files
{
    public class NGFolder : IFolder
    {
        private readonly IStorageFolder folder;

        public NGFolder(IStorageFolder folder)
        {
            this.folder = folder;
        }

        public string Name { get { return folder.Name; } }

        public string Path { get { return folder.Path; } }

        public string DisplayName { get { return (folder as IStorageItemProperties)?.DisplayName; } }

        public string DisplayType { get { return (folder as IStorageItemProperties)?.DisplayType; } }

        public StorageItemType Type { get; } = StorageItemType.Folder;

        public async Task<IFile> GetFile(string name)
        {
            var file = await folder.GetFileAsync(name);            return new NGFile(file);
        }

        public async Task<IReadOnlyList<IFile>> GetFiles()
        {
            var ngFiles = new List<IFile>();            var files = await folder.GetFilesAsync();            foreach (var file in files)            {                ngFiles.Add(new NGFile(file));            }            return new ReadOnlyCollection<IFile>(ngFiles);
        }

        public async Task<IFolder> GetFolder(string name)
        {
            var f = await folder.GetFolderAsync(name);            return new NGFolder(f);
        }

        public async Task<IReadOnlyList<IFolder>> GetFolders()
        {
            var ngFolders = new List<IFolder>();            var folders = await folder.GetFoldersAsync();            foreach (var folder in folders)            {                var f = new NGFolder(folder);                ngFolders.Add(f);            }            return new ReadOnlyCollection<IFolder>(ngFolders);
        }
    }
}
