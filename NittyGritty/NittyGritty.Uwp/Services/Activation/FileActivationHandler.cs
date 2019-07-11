using NittyGritty.Uwp.Declarations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Storage;
using Windows.Storage.AccessCache;

namespace NittyGritty.Uwp.Services.Activation
{
    public class FileActivationHandler : ActivationHandler<FileActivatedEventArgs>
    {
        private static Dictionary<string, FileTypeAssociation> _fileTypeAssociations;

        static FileActivationHandler()
        {
            _fileTypeAssociations = new Dictionary<string, FileTypeAssociation>();
        }

        public FileActivationHandler(bool createsNewView)
        {
            Strategy = createsNewView ? ActivationStrategy.NewView : ActivationStrategy.Normal;
        }

        public static ReadOnlyDictionary<string, FileTypeAssociation> FileTypeAssociations
        {
            get { return new ReadOnlyDictionary<string, FileTypeAssociation>(_fileTypeAssociations); }
        }

        public static void AddAssociation(FileTypeAssociation association)
        {
            if(!_fileTypeAssociations.ContainsKey(association.FileType))
            {
                _fileTypeAssociations.Add(association.FileType, association);
            }
            else
            {
                throw new ArgumentException("You only have to register for a file type association once.");
            }
        }

        public Func<IStorageFile, Task> UnknownFile { get; set; }

        public override async Task HandleAsync(FileActivatedEventArgs args)
        {
            foreach (var file in args.Files)
            {
                if(file is IStorageFile storageFile)
                {
                    if(_fileTypeAssociations.TryGetValue(storageFile.FileType, out var fileTypeAssociation))
                    {
                        await fileTypeAssociation.Run(storageFile);
                    }
                    else
                    {
                        await UnknownFile?.Invoke(storageFile);
                    }
                }
            }
        }
    }
}
