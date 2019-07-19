using NittyGritty.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NittyGritty.Views.Events
{
    public class PickedFileChangedEventArgs : EventArgs
    {
        public PickedFileChangedAction Action { get; private set; }

        public IReadOnlyList<PickedFile> AddedFiles { get; private set; }

        public IReadOnlyList<PickedFile> RemovedFiles { get; private set; }

        public PickedFileChangedEventArgs(PickedFileChangedAction action, PickedFile file)
        {
            Initialize(action, new PickedFile[] { file });
        }

        public PickedFileChangedEventArgs(PickedFileChangedAction action, IList<PickedFile> files)
        {
            Initialize(action, files);
        }

        private void Initialize(PickedFileChangedAction action, IList<PickedFile> files)
        {
            Action = action;

            if(action == PickedFileChangedAction.Add)
            {
                AddedFiles = new ReadOnlyCollection<PickedFile>(files);
            }
            else if(action == PickedFileChangedAction.Remove)
            {
                RemovedFiles = new ReadOnlyCollection<PickedFile>(files);
            }
        }
    }

    public delegate void PickedFileChangedEventHandler(object sender, PickedFileChangedEventArgs e);

}
