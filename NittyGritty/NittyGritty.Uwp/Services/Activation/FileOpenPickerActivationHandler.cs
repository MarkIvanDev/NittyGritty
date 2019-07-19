using NittyGritty.Models;
using NittyGritty.Utilities;
using NittyGritty.Views;
using NittyGritty.Views.Events;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Pickers.Provider;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp.Services.Activation
{
    public class FileOpenPickerActivationHandler : ActivationHandler<FileOpenPickerActivatedEventArgs>
    {
        private readonly object syncObject = new object();
        private FileOpenPickerUI fileOpenPickerUI;

        public FileOpenPickerActivationHandler(Type openPickerView) : base(ActivationStrategy.Hosted)
        {
            OpenPickerView = openPickerView;
        }

        public Type OpenPickerView { get; }

        protected override async Task HandleInternal(FileOpenPickerActivatedEventArgs args)
        {
            fileOpenPickerUI = args.FileOpenPickerUI;

            // Since this is marked as a Hosted activation, it is assumed that the current window's content has been initialized with a frame by the ActivationService
            if (Window.Current.Content is Frame frame)
            {
                var settings = new FilePickerSettings(
                    fileOpenPickerUI.SelectionMode == FileSelectionMode.Multiple,
                    fileOpenPickerUI.AllowedFileTypes);
                frame.Navigate(OpenPickerView, settings);
                if(frame.Content is Page page)
                {
                    if (page.DataContext is IFileOpenPicker fileOpenPickerContext)
                    {
                        fileOpenPickerContext.PickedFileChanged += PickedFileChanged;
                    }
                }
            }
            await Task.CompletedTask;
        }

        private async void PickedFileChanged(object sender, PickedFileChangedEventArgs e)
        {
            if(e.Action == PickedFileChangedAction.Add)
            {
                foreach (var f in e.AddedFiles)
                {
                    IStorageFile file = null;
                    if(f.Source == PickedFileSource.Local)
                    {
                        file = await StorageFile.GetFileFromPathAsync(f.Path);
                    }
                    else if(f.Source == PickedFileSource.Uri)
                    {
                        var uri = new Uri(f.Path);
                        file = await StorageFile.CreateStreamedFileFromUriAsync(Path.GetFileName(f.Path), uri, RandomAccessStreamReference.CreateFromUri(uri));
                    }
                    else
                    {
                        throw new ArgumentException("Unknown File source.", nameof(file));
                    }
                    
                    lock(syncObject)
                    {
                        if(fileOpenPickerUI.CanAddFile(file))
                        {
                            fileOpenPickerUI.AddFile(f.Path, file);
                        }
                    }
                }
            }
            else if(e.Action == PickedFileChangedAction.Remove)
            {
                foreach (var f in e.RemovedFiles)
                {   
                    lock (syncObject)
                    {
                        if (fileOpenPickerUI.ContainsFile(f.Path))
                        {
                            fileOpenPickerUI.RemoveFile(f.Path);
                        }
                    }
                }
            }
        }
    }
}
