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
        private IFileSavePicker fileSavePickerContext;

        public FileSavePickerActivationHandler(Type savePickerView) : base(ActivationStrategy.Hosted)
        {
            SavePickerView = savePickerView;
        }

        public Type SavePickerView { get; }

        protected override async Task HandleInternal(FileSavePickerActivatedEventArgs args)
        {
            // Since this is marked as a Hosted activation, it is assumed that the current window's content has been initialized with a frame by the ActivationService
            if (Window.Current.Content is Frame frame)
            {
                var settings = new FilePickerSettings(false, args.FileSavePickerUI.AllowedFileTypes);
                frame.Navigate(SavePickerView, settings);
                if (frame.Content is Page page)
                {
                    fileSavePickerContext = page.DataContext as IFileSavePicker;
                    if (fileSavePickerContext != null)
                    {
                        args.FileSavePickerUI.TargetFileRequested += OnTargetFileRequested;
                    }
                }
            }
            await Task.CompletedTask;
        }

        private async void OnTargetFileRequested(FileSavePickerUI sender, TargetFileRequestedEventArgs args)
        {
            var deferral = args.Request.GetDeferral();
            await Window.Current.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                // This method will be called on the app's UI thread, which allows for actions like manipulating
                // the UI or showing error dialogs
                try
                {
                    var folder = await StorageFolder.GetFolderFromPathAsync(fileSavePickerContext.GetCurrentPath());
                    var file = await folder.CreateFileAsync(sender.FileName, CreationCollisionOption.GenerateUniqueName);

                    args.Request.TargetFile = file;
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
                finally
                {
                    deferral.Complete();
                }
            });
        }
    }
}
