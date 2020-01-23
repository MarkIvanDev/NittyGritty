using NittyGritty.Platform.Storage;
using NittyGritty.Platform.Payloads;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace NittyGritty.Uwp.Payloads
{
    public class FilePayload : IFilePayload
    {
        private readonly IEnumerable<IStorageFile> files;

        public FilePayload(string action, IEnumerable<IStorageFile> files)
        {
            if(string.IsNullOrWhiteSpace(action))
            {
                throw new ArgumentException("Verb cannot be null, empty or whitespace");
            }

            Action = action;
            this.files = files ?? throw new ArgumentNullException(nameof(files));
            AvailableFileTypes = new ReadOnlyCollection<string>(files.Select(f => f.FileType).Distinct().ToList());
        }

        public string Action { get; }

        public IReadOnlyCollection<string> AvailableFileTypes { get; }

        public async Task<IReadOnlyCollection<IFile>> GetFiles()
        {
            var list = new List<NGFile>();
            foreach (var file in files)
            {
                var ngFile = new NGFile(file);
                list.Add(ngFile);
            }
            return await Task.FromResult(new ReadOnlyCollection<NGFile>(list));
        }

        public async Task<IFile> GetFile(string fileName)
        {
            var file = files.FirstOrDefault(f => Path.GetFileName(f.Path) == fileName);
            if (file != null)
            {
                var ngFile = new NGFile(file);
                return await Task.FromResult(ngFile);
            }
            return null;
        }
    }
}
