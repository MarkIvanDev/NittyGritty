using NittyGritty.Platform.Files;
using NittyGritty.Platform.Payloads;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace NittyGritty.UI.Platform.Payloads
{
    public class FilePayload : IFilePayload
    {
        private readonly IEnumerable<IStorageFile> files;
        private readonly IFileProcessor processor;

        public FilePayload(string action, IEnumerable<IStorageFile> files, IFileProcessor processor)
        {
            if(string.IsNullOrWhiteSpace(action))
            {
                throw new ArgumentException("Verb cannot be null, empty or whitespace");
            }

            Action = action;
            this.files = files ?? throw new ArgumentNullException(nameof(files));
            this.processor = processor ?? throw new ArgumentNullException(nameof(processor));
            AvailableFileTypes = new ReadOnlyCollection<string>(files.Select(f => f.FileType).Distinct().ToList());
        }

        public string Action { get; }

        public IReadOnlyCollection<string> AvailableFileTypes { get; }

        public async Task<IReadOnlyCollection<NGFile>> Extract()
        {
            var list = new List<NGFile>();
            foreach (var file in files)
            {
                var ngFile = new NGFile();
                ngFile.Path = file.Path;
                ngFile.Content = await processor.Process(file);
                list.Add(ngFile);
            }
            return new ReadOnlyCollection<NGFile>(list);
        }
    }
}
