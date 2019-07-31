using NittyGritty.Platform.Payloads;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace NittyGritty.Uwp.Platform
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
            FilePaths = new ReadOnlyCollection<string>(files.Select(f => f.Path).ToList());
        }

        public string Action { get; }

        public IReadOnlyCollection<string> AvailableFileTypes { get; }

        public IReadOnlyCollection<string> FilePaths { get; }

        public async Task<IReadOnlyCollection<object>> Extract()
        {
            var list = new List<object>();
            foreach (var file in files)
            {
                list.Add(await processor.Process(file));
            }
            return new ReadOnlyCollection<object>(list);
        }
    }
}
