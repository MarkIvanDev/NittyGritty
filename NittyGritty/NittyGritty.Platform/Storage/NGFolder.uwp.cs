﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace NittyGritty.Platform.Storage
{
    public class NGFolder : IFolder
    {
        public NGFolder(IStorageFolder folder)
        {
            Context = folder;
        }

        object IStorageItem.Context { get { return Context; } }

        public IStorageFolder Context { get; }

        public string Name { get { return Context.Name; } }

        public string Path { get { return Context.Path; } }

        public string DisplayName { get { return (Context as IStorageItemProperties)?.DisplayName; } }

        public string DisplayType { get { return (Context as IStorageItemProperties)?.DisplayType; } }

        public StorageItemType Type { get; } = StorageItemType.Folder;

        public async Task<IFile> GetFile(string name)
        {
            var file = await Context.GetFileAsync(name);
        }

        public async Task<IReadOnlyList<IFile>> GetFiles()
        {
            var ngFiles = new List<IFile>();
        }

        public async Task<IFolder> GetFolder(string name)
        {
            var f = await Context.GetFolderAsync(name);
        }

        public async Task<IReadOnlyList<IFolder>> GetFolders()
        {
            var ngFolders = new List<IFolder>();
        }
    }
}