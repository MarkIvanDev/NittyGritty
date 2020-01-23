using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Platform.Storage
{
    public interface IStorageItem
    {
        string Name { get; }

        string DisplayName { get; }

        string DisplayType { get; }

        StorageItemType Type { get; }

        string Path { get; }
    }

    public enum StorageItemType
    {
        Other = 0,
        File = 1,
        Folder = 2
    }
}
