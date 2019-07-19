using NittyGritty.Uwp.Operations;
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
        private readonly Dictionary<string, FileOperation> operations;

        public FileActivationHandler(params FileOperation[] operations) : base(ActivationStrategy.Normal)
        {
            this.operations = new Dictionary<string, FileOperation>();
            foreach (var operation in operations)
            {
                this.operations.Add(operation.FileType, operation);
            }
            Operations = new ReadOnlyDictionary<string, FileOperation>(this.operations);
        }

        public ReadOnlyDictionary<string, FileOperation> Operations { get; }

        protected override async Task HandleInternal(FileActivatedEventArgs args)
        {
            foreach (var file in args.Files)
            {
                if(file is StorageFile storageFile)
                {
                    if(operations.TryGetValue(storageFile.FileType, out var operation))
                    {
                        await operation.Run(args, storageFile, NavigationContext);
                    }
                    else
                    {
                        // We should not reach this part. Please check if you have added all the file types that this app can handle
                    }
                }
            }
        }
    }
}
