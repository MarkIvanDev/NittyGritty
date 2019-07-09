using NittyGritty.Uwp.Declarations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Storage;

namespace NittyGritty.Uwp.Services.Activation
{
    public class FileActivationHandler : ActivationHandler<FileActivatedEventArgs>
    {
        private readonly List<FileTypeAssociation> fileTypeAssociations;

        public FileActivationHandler(IEnumerable<FileTypeAssociation> fileTypeAssociations, bool createsNewView)
        {
            Strategy = createsNewView ? ActivationStrategy.NewView : ActivationStrategy.Normal;

            foreach (var fta in fileTypeAssociations ?? Enumerable.Empty<FileTypeAssociation>())
            {
                if(this.fileTypeAssociations.Any(f => f.FileType == fta.FileType))
                {
                    // You only have to register for a file type once
                    continue;
                }
                this.fileTypeAssociations.Add(fta);
            }
        }

        public Func<IStorageFile, Task> UnknownFile { get; set; }

        public override async Task HandleAsync(FileActivatedEventArgs args)
        {
            foreach (var file in args.Files)
            {
                if(file is IStorageFile storageFile)
                {
                    var fileTypeAssociation = fileTypeAssociations.SingleOrDefault(fta => fta.FileType.Equals(storageFile.FileType));
                    if(fileTypeAssociation == null)
                    {
                        await UnknownFile?.Invoke(storageFile);
                        continue;
                    }
                    await fileTypeAssociation.Run(storageFile);
                }
            }
        }
    }
}
