using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp.Operations
{
    public class FileOperation
    {
        /// <summary>
        /// Creates a FileOperation to handle the file that activated the app
        /// </summary>
        /// <param name="fileType">The file type this FileOperation can handle. Cannot be null, empty, or whitespace</param>
        /// <param name="callback"></param>
        public FileOperation(string fileType, Type view)
        {
            if(string.IsNullOrWhiteSpace(fileType))
            {
                throw new ArgumentException("File type cannot be null, empty, or whitespace.", nameof(fileType));
            }

            FileType = fileType;
            View = view;
        }

        public string FileType { get; }

        public Type View { get; }

        public async Task Run(FileActivatedEventArgs args, StorageFile file, Frame frame)
        {
            var data = await ProcessFile(file);
            frame.Navigate(View, data);
        }

        protected virtual async Task<object> ProcessFile(StorageFile file)
        {
            return await file.OpenStreamForReadAsync();
        }
    }
}
