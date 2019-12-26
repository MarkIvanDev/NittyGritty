using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Files;
using NittyGritty.Platform.Services;
using Windows.Storage.Pickers;

namespace NittyGritty.Uwp.Services
{
    public class PickerService : IPickerService
    {
        public async Task<NGFile> Open(IList<string> fileTypes)
        {
            var picker = new FileOpenPicker
            {
                CommitButtonText = "Open",
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
                ViewMode = PickerViewMode.List
            };

            foreach (var item in fileTypes)
            {
                picker.FileTypeFilter.Add(item);
            }

            var file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                var path = file.Path;
                var stream = await file.OpenStreamForReadAsync();
                return new NGFile(path, stream);
            }
            else
            {
                return null;
            }
        }

        public async Task<NGFile> Save(IDictionary<string, IList<string>> fileTypes)
        {
            var picker = new FileSavePicker()
            {
                CommitButtonText = "Save",
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };
            foreach (var item in fileTypes)
            {
                picker.FileTypeChoices.Add(item);
            }

            var file = await picker.PickSaveFileAsync();
            if (file != null)
            {
                var path = file.Path;
                var stream = await file.OpenStreamForWriteAsync();
                return new NGFile(path, stream);
            }
            else
            {
                return null;
            }
        }

        public async Task SaveAndOpen(IDictionary<string, IList<string>> fileTypes, Func<Stream, Task> writer)
        {
            var picker = new FileSavePicker()
            {
                CommitButtonText = "Save",
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };
            foreach (var item in fileTypes)
            {
                picker.FileTypeChoices.Add(item);
            }

            var file = await picker.PickSaveFileAsync();
            if (file != null)
            {
                var stream = await file.OpenStreamForWriteAsync();
                await writer?.Invoke(stream);
                await Windows.System.Launcher.LaunchFileAsync(file);
            }
        }
    }
}
