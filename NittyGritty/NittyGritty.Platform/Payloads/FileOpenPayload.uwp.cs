using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Models;
using Windows.Storage;
using Windows.Storage.Pickers.Provider;
using Windows.Storage.Streams;

namespace NittyGritty.Platform.Payloads
{
    public class FileOpenPayload : IFileOpenPayload
    {
        private readonly object syncObject;
        private readonly FileOpenPickerUI fileOpenPickerUI;

        public FileOpenPayload(FileOpenPickerUI fileOpenPickerUI)
        {
            this.syncObject = new object();
            this.fileOpenPickerUI = fileOpenPickerUI;
            IsMultiSelect = fileOpenPickerUI.SelectionMode == FileSelectionMode.Multiple;
            FileTypes = fileOpenPickerUI.AllowedFileTypes;
        }

        public bool IsMultiSelect { get; }

        public IReadOnlyList<string> FileTypes { get; }

        public async Task Add(PickedFile file)
        {
            IStorageFile f = null;
            if (file.Source == PickedFileSource.Local)
            {
                f = await StorageFile.GetFileFromPathAsync(file.Path);
            }
            else if (file.Source == PickedFileSource.Uri)
            {
                var uri = new Uri(file.Path);
                f = await StorageFile.CreateStreamedFileFromUriAsync(Path.GetFileName(file.Path), uri, RandomAccessStreamReference.CreateFromUri(uri));
            }
            else
            {
                throw new ArgumentException("Unknown File source.", nameof(f));
            }

            lock (syncObject)
            {
                if (fileOpenPickerUI.CanAddFile(f))
                {
                    fileOpenPickerUI.AddFile(file.Path, f);
                }
            }
        }

        public async Task Add(IEnumerable<PickedFile> files)
        {
            foreach (var f in files)
            {
                await Add(f);
            }
        }

        public async Task Remove(PickedFile file)
        {
            lock (syncObject)
            {
                if (fileOpenPickerUI.ContainsFile(file.Path))
                {
                    fileOpenPickerUI.RemoveFile(file.Path);
                }
            }
            await Task.CompletedTask;
        }

        public async Task Remove(IEnumerable<PickedFile> files)
        {
            foreach (var f in files)
            {
                await Remove(f);
            }
        }
    }
}
