using NittyGritty.Models;
using NittyGritty.Views;
using NittyGritty.Views.Events;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
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
        private IFileOpenPicker fileOpenPickerContext;

        public FileOpenPickerActivationHandler(Type openPickerView)
        {
            Strategy = ActivationStrategy.Picker;
            OpenPickerView = openPickerView;
        }

        public Type OpenPickerView { get; }

        public Func<FileOpenPickerUI, Page, Task> IntegratePicker { get; set; }

        public Func<PickedFile, Task<IStorageFile>> UnknownFileSource { get; set; }

        public override async Task HandleAsync(FileOpenPickerActivatedEventArgs args)
        {
            fileOpenPickerUI = args.FileOpenPickerUI;

            // Since this is marked as a Picker activation, it is assumed that the current window's content has been initialized with a frame by the ActivationService
            if (Window.Current.Content is Frame frame)
            {
                var settings = new FilePickerSettings(
                    fileOpenPickerUI.SelectionMode == FileSelectionMode.Multiple,
                    fileOpenPickerUI.AllowedFileTypes);
                frame.Navigate(OpenPickerView, settings);
                if(frame.Content is Page page)
                {
                    fileOpenPickerContext = page.DataContext as IFileOpenPicker;
                    if(fileOpenPickerContext != null)
                    {
                        fileOpenPickerUI.Closing += FileOpenPickerUI_Closing;
                        fileOpenPickerContext.PickFileChanged += Context_PickFileChanged;
                    }
                    else
                    {
                        await IntegratePicker?.Invoke(fileOpenPickerUI, page);
                    }
                }
            }
        }

        private async void Context_PickFileChanged(object sender, PickedFileChangedEventArgs e)
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
                        file = await UnknownFileSource?.Invoke(f);
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

        private void FileOpenPickerUI_Closing(FileOpenPickerUI sender, PickerClosingEventArgs args)
        {
            if(fileOpenPickerContext != null)
            {
                fileOpenPickerContext.PickFileChanged -= Context_PickFileChanged;
                fileOpenPickerContext = null;
            }
            fileOpenPickerUI.Closing -= FileOpenPickerUI_Closing;
        }

    }
}
