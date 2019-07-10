using NittyGritty.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Storage;
using Windows.Storage.Pickers.Provider;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp.Services.Activation
{
    public class FileSavePickerActivationHandler : ActivationHandler<FileSavePickerActivatedEventArgs>
    {
        private FileSavePickerUI fileSavePickerUI;
        private IFileSavePicker fileSavePickerContext;

        public FileSavePickerActivationHandler(Type savePickerView)
        {
            Strategy = ActivationStrategy.Picker;
            SavePickerView = savePickerView;
        }

        public Type SavePickerView { get; }

        public Func<FileSavePickerUI, Page, Task> IntegratePicker { get; set; }

        public Func<string, Task<IStorageFile>> GenerateFile { get; set; }

        public override async Task HandleAsync(FileSavePickerActivatedEventArgs args)
        {
            fileSavePickerUI = args.FileSavePickerUI;

            // Since this is marked as a Picker activation, it is assumed that the current window's content has been initialized with a frame by the ActivationService
            if (Window.Current.Content is Frame frame)
            {
                var settings = new FilePickerSettings(false, fileSavePickerUI.AllowedFileTypes);
                frame.Navigate(SavePickerView, settings);
                if (frame.Content is Page page)
                {
                    fileSavePickerContext = page.DataContext as IFileSavePicker;
                    if (fileSavePickerContext != null)
                    {
                        fileSavePickerUI.TargetFileRequested += FileSavePickerUI_TargetFileRequested;
                    }
                    else
                    {
                        await IntegratePicker?.Invoke(fileSavePickerUI, page);
                    }
                }
            }
        }

        private async void FileSavePickerUI_TargetFileRequested(FileSavePickerUI sender, TargetFileRequestedEventArgs args)
        {
            var deferral = args.Request.GetDeferral();
            if (GenerateFile == null)
            {
                await Window.Current.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                {
                    // This method will be called on the app's UI thread, which allows for actions like manipulating
                    // the UI or showing error dialogs
                    try
                    {
                        var folder = await StorageFolder.GetFolderFromPathAsync(fileSavePickerContext.GetCurrentPath());
                        var file = await folder.CreateFileAsync(fileSavePickerUI.FileName, CreationCollisionOption.FailIfExists);

                        args.Request.TargetFile = file;
                        fileSavePickerUI.TargetFileRequested -= FileSavePickerUI_TargetFileRequested;
                        deferral.Complete();
                    }
                    catch (ArgumentException)
                    {
                        // originates from folder access
                        var errorDialog = new MessageDialog(
                            "The save picker app you chose gave an invalid folder path. Please check again.");
                        await errorDialog.ShowAsync();

                        args.Request.TargetFile = null;
                        deferral.Complete();
                    }
                    catch (UnauthorizedAccessException)
                    {
                        // can originate from folder access or file creation
                        var errorDialog = new MessageDialog(
                            "You do not have permission to save a file in this folder. Please check your app permissions.");
                        await errorDialog.ShowAsync();

                        args.Request.TargetFile = null;
                        deferral.Complete();
                    }
                    catch (FileNotFoundException)
                    {
                        // either folder does not exist or file name is invalid
                        var errorDialog = new MessageDialog(
                            "The file name may contain invalid characters. Please check your file name again.");
                        await errorDialog.ShowAsync();

                        args.Request.TargetFile = null;
                        deferral.Complete();
                    }
                    catch (Exception ex)
                    {
                        var errorDialog = new MessageDialog(
                            $"An error has occurred: {ex.Message}");
                        await errorDialog.ShowAsync();

                        args.Request.TargetFile = null;
                        deferral.Complete();
                    }
                });
            }
            else
            {
                var fullPath = Path.Combine(fileSavePickerContext.GetCurrentPath(), fileSavePickerUI.FileName);
                args.Request.TargetFile = await GenerateFile.Invoke(fullPath);
                deferral.Complete();
            }
            
        }
    }
}
