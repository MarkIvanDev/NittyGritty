using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Storage;
using NittyGritty.Services.Core;
using Windows.Storage.Pickers;

namespace NittyGritty.Uwp.Services
{
    public class PickerService : IPickerService
    {
        public async Task<IFile> OpenFile(IList<string> fileTypes)
        {
            var picker = new FileOpenPicker();
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

        public async Task<IList<IFile>> OpenFiles(IList<string> fileTypes)
        {
            var picker = new FileOpenPicker();
            foreach (var item in fileTypes)
            {
                picker.FileTypeFilter.Add(item);
            }

            var files = await picker.PickMultipleFilesAsync();
            return files.Select(f => (IFile)new NGFile(f)).ToList();
        }

        public async Task<IFile> SaveFile(IDictionary<string, IList<string>> fileTypes)
        {
            var picker = new FileSavePicker();
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
    
        public async Task<IFolder> OpenFolder()
        {
            var picker = new FolderPicker();
            picker.FileTypeFilter.Add("*");

            var folder = await picker.PickSingleFolderAsync();
            if (folder != null)
            {
                return new NGFolder(folder);
            }
            else
            {
                return null;
            }
        }
    
    }
}
