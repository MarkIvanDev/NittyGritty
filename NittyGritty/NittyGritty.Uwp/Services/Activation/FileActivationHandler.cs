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

        public FileActivationHandler(IEnumerable<FileTypeAssociation> fileTypeAssociations)
        {
            foreach (var fta in fileTypeAssociations ?? Enumerable.Empty<FileTypeAssociation>())
            {
                this.fileTypeAssociations.Add(fta);
            }
        }

        public override async Task HandleAsync(FileActivatedEventArgs args)
        {
            foreach (var file in args.Files)
            {
                if(file is IStorageFile storageFile)
                {
                    var fileTypeAssociation = fileTypeAssociations.SingleOrDefault(fta => fta.FileType.Equals(storageFile.FileType)) ??
                        throw new InvalidOperationException($"No file type association for: {storageFile.FileType}");
                    await fileTypeAssociation.Run(storageFile);
                }
            }
        }

        public override bool CanHandle(FileActivatedEventArgs args)
        {
            return base.CanHandle(args);
        }
    }
}
