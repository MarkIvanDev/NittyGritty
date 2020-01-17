using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Files;
using Windows.Storage.Pickers;

namespace NittyGritty.Services
{
    public partial class PickerService
    {
        async Task<IFile> PlatformOpen(IList<string> fileTypes)
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
                return new NGFile(file);
            }
            else
            {
                return null;
            }
        }

        async Task<IFile> PlatformSave(IDictionary<string, IList<string>> fileTypes)
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
                return new NGFile(file);
            }
            else
            {
                return null;
            }
        }
    }
}
