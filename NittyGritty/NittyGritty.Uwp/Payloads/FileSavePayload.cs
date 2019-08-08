using NittyGritty.Platform.Payloads;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers.Provider;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;

namespace NittyGritty.Uwp.Payloads
{
    public class FileSavePayload : IFileSavePayload
    {
        private readonly FileSavePickerUI fileSavePickerUI;

        public FileSavePayload(FileSavePickerUI fileSavePickerUI)
        {
            this.fileSavePickerUI = fileSavePickerUI;
            FileTypes = fileSavePickerUI.AllowedFileTypes;
            this.fileSavePickerUI.TargetFileRequested += OnTargetFileRequested;
        }

        public IReadOnlyList<string> FileTypes { get; }

        public string SavePath { get; set; }

        private async void OnTargetFileRequested(FileSavePickerUI sender, TargetFileRequestedEventArgs args)
        {
            var deferral = args.Request.GetDeferral();
            await Window.Current.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                // This method will be called on the app's UI thread, which allows for actions like manipulating
                // the UI or showing error dialogs
                try
                {
                    var folder = await StorageFolder.GetFolderFromPathAsync(SavePath);
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
