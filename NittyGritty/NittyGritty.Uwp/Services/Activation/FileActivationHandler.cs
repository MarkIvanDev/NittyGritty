using NittyGritty.Uwp.Operations;
using NittyGritty.Views.Payloads;
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

        /// <summary>
        /// Creates a FileActivationHandler for a specific verb with its operations
        /// </summary>
        /// <param name="verb">The verb this File Activation can handle. Cannot be null, empty, or whitespace.</param>
        /// <param name="operations">Operations that can run for this verb</param>
        public FileActivationHandler(params FileOperation[] operations) : base(ActivationStrategy.Normal)
        {
            this.operations = new Dictionary<string, FileOperation>();
            foreach (var operation in operations)
            {
                this.operations.Add(operation.Verb, operation);
            }
            Operations = new ReadOnlyDictionary<string, FileOperation>(this.operations);
        }

        public ReadOnlyDictionary<string, FileOperation> Operations { get; }

        protected override async Task HandleInternal(FileActivatedEventArgs args)
        {
            if (operations.TryGetValue(args.Verb, out var operation))
            {
                await operation.Run(args, NavigationContext);
            }
            else
            {
                // We should not reach this part. Please check if you have registered all of the Verbs this app handles
            }
        }

    }
}
