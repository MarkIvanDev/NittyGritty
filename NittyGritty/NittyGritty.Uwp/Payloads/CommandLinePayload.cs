using NittyGritty.Models;
using NittyGritty.Platform.Files;
using NittyGritty.Platform.Payloads;
using NittyGritty.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Storage;

namespace NittyGritty.Uwp.Payloads
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
            var parsedCommand = CommandLineUtilities.Parse(operation.Arguments);
            Command = parsedCommand.Command;
            Parameters = parsedCommand.Parameters;
        }

        public string OriginalArguments { get; }

        public string CurrentDirectory { get; }

        public string Command { get; }

        public QueryString Parameters { get; }

        public async Task<IReadOnlyCollection<NGFile>> GetFiles(bool canWrite)
        {
            var files = await folder.GetFilesAsync();
            var ngFiles = new List<NGFile>();
            foreach (var file in files)
            {
                var content = canWrite ? await file.OpenAsync(FileAccessMode.ReadWrite) : await file.OpenAsync(FileAccessMode.Read);
                var ngFile = new NGFile(file.Path, canWrite ? content.AsStreamForWrite() : content.AsStreamForRead());
                ngFiles.Add(ngFile);
            }
            return new ReadOnlyCollection<NGFile>(ngFiles);
        }

        public async Task<NGFile> GetFile(string fileName, bool canWrite)
        {
            var files = await folder.GetFilesAsync();
            var file = files.FirstOrDefault(f => Path.GetFileName(f.Path) == fileName);
            if (file != null)
            {
                var content = canWrite ? await file.OpenAsync(FileAccessMode.ReadWrite) : await file.OpenAsync(FileAccessMode.Read);
                var ngFile = new NGFile(file.Path, canWrite ? content.AsStreamForWrite() : content.AsStreamForRead());
                return ngFile;
            }
            return null;
        }

        public async Task<NGFile> WriteFile(string fileName)
        {
            var file = await folder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
            return new NGFile(file.Path, await file.OpenStreamForWriteAsync());
        }
    }
}
