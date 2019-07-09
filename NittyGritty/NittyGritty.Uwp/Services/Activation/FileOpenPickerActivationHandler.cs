using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Storage.Pickers.Provider;

namespace NittyGritty.Uwp.Services.Activation
{
    public class FileOpenPickerActivationHandler : ActivationHandler<FileOpenPickerActivatedEventArgs>
    {
        private ObservableCollection<string> openedFilePaths;
        private FileOpenPickerUI fileOpenPickerUI;

        public FileOpenPickerActivationHandler()
        {
            Strategy = ActivationStrategy.Picker;
        }

        public override async Task HandleAsync(FileOpenPickerActivatedEventArgs args)
        {
            openedFilePaths = new ObservableCollection<string>();
            openedFilePaths.CollectionChanged += OpenedFilePaths_CollectionChanged;

            fileOpenPickerUI = args.FileOpenPickerUI;
            fileOpenPickerUI.Closing += FileOpenPickerUI_Closing;


        }

        private void OpenedFilePaths_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            
        }

        private void FileOpenPickerUI_Closing(FileOpenPickerUI sender, PickerClosingEventArgs args)
        {
            openedFilePaths.CollectionChanged -= OpenedFilePaths_CollectionChanged;
        }
    }
}
