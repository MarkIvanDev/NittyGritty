using NittyGritty.Views.Payloads;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp.Operations
{
    public class FileOperation : KeyViewOperation<FilePayload>
    {
        private readonly Dictionary<string, FileProcessor> processors;

        /// <summary>
        /// Creates a FileOperation to handle the file that activated the app
        /// </summary>
        /// <param name="verb">The verb this FileOperation can handle. Cannot be null, empty, or whitespace
        /// Default value of open means the FileOperation is activated through file launch</param>
        public FileOperation(string verb = "open") : base()
        {
            if(string.IsNullOrWhiteSpace(verb))
            {
                throw new ArgumentException("Verb cannot be null, empty, or whitespace.", nameof(verb));
            }

            Verb = verb;
            processors = new Dictionary<string, FileProcessor>();
        }

        public string Verb { get; }

        public override void Configure(string key, Type view, Predicate<FilePayload> createsNewView = null)
        {
            Configure(key, view, new FileProcessor(key), createsNewView);
        }

        /// <summary>
        /// Configures the paths that this scheme can handle with the appropriate view
        /// </summary>
        /// <param name="fileType">The file type that this verb can handle.
        /// A file type with a value of * will be used as fallback for unknown file types</param>
        /// <param name="view">The type of the view that the file type leads to</param>
        public void Configure(string fileType, Type view, FileProcessor processor, Predicate<FilePayload> createsNewView = null)
        {
            if (fileType == string.Empty)
            {
                throw new ArgumentException("File Type cannot be empty");
            }

            if (Path.GetExtension(fileType) != fileType && fileType != "*")
            {
                throw new ArgumentException("File Type is not valid");
            }

            if (processor == null)
            {
                throw new ArgumentNullException(nameof(processor), "File Processor cannot be null");
            }

            if (fileType != processor.FileType)
            {
                throw new ArgumentException("File Type does not match the Processor File Type");
            }

            base.Configure(fileType, view, createsNewView);
            
            processors.Add(
                fileType,
                processor);
        }

        public async Task Run(FileActivatedEventArgs args, Frame frame)
        {
            if(args.Verb != Verb)
            {
                return;
            }
            
            var filesData = new Dictionary<string, List<object>>();
            foreach (var file in args.Files)
            {
                if (file is IStorageFile storageFile)
                {
                    var d = await ProcessFile(storageFile);
                    if (filesData.TryGetValue(storageFile.FileType, out var fileData))
                    {
                        fileData.Add(d);
                    }
                    else
                    {
                        filesData.Add(storageFile.FileType, new List<object>() { d });
                    }
                }
                else if (file is IStorageFolder folder)
                {
                    // TODO: fetch files in folder and operate on them
                    var folderFiles = await folder.GetFilesAsync();
                    foreach (var item in folderFiles)
                    {
                        var d = await ProcessFile(item);
                        if (filesData.TryGetValue(item.FileType, out var fileData))
                        {
                            fileData.Add(d);
                        }
                        else
                        {
                            filesData.Add(item.FileType, new List<object>() { d });
                        }
                    }
                }
            }

            foreach (var fileType in filesData.Keys)
            {
                var payload = new FilePayload(Verb, fileType, filesData[fileType]);

                var configuration = GetConfiguration(fileType);
                await configuration.Run(payload, args.CurrentlyShownApplicationViewId, frame);
            }
        }

        private async Task<object> ProcessFile(IStorageFile file)
        {
            if(processors.TryGetValue(file.FileType, out var processor))
            {
                return await processor.Process(file);
            }
            else
            {
                if(processors.TryGetValue("*", out var fallbackProcessor))
                {
                    return await fallbackProcessor.Process(file);
                }
                else
                {
                    throw new ArgumentException(
                        string.Format(
                            "No processor for file type: {0}. Did you forget to call FileOperation.Configure?",
                            file.FileType));
                }
            }
        }
    }
}
