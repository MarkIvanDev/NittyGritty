using NittyGritty.Models;
using NittyGritty.Platform.Files;
using NittyGritty.Platform.Payloads;
using NittyGritty.Utilities;
using NittyGritty.Uwp.Platform.Files;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Storage;

namespace NittyGritty.Uwp.Platform.Payloads
{
    public class CommandLinePayload : ICommandLinePayload
    {
        private readonly CommandLineActivationOperation operation;
        private readonly IStorageFolder folder;

        public CommandLinePayload(CommandLineActivationOperation operation, IStorageFolder folder)
        {
            this.operation = operation;
            this.folder = folder;
            OriginalArguments = operation.Arguments;
            (Command, Parameters) = CommandLineUtilities.Parse(operation.Arguments);
        }

        public string OriginalArguments { get; }

        public string CurrentDirectory { get; }

        public string Command { get; }

        public QueryString Parameters { get; }

        public async Task<IReadOnlyCollection<NGFile>> GetFiles()
        {
            var streamProcessor = new StreamFileProcessor();
            var files = await folder.GetFilesAsync();

            var ngFiles = new List<NGFile>();
            foreach (var file in files)
            {
                var ngFile = new NGFile();
                ngFile.Path = file.Path;
                ngFile.Content = await streamProcessor.Process(file);
                ngFiles.Add(ngFile);
            }
            return new ReadOnlyCollection<NGFile>(ngFiles);
        }
    }
}
