using NittyGritty.Platform.Files;
using NittyGritty.Uwp.Platform;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.Uwp.Activation.Operations.Jobs
{
    public class FileConfiguration
    {
        /// <summary>
        /// Creates a configuration for the FileOperation
        /// </summary>
        /// <param name="fileType">The file type that this configuration is for.
        /// A file type with a value of * will be used as fallback for unknown file types</param>
        /// <param name="view"></param>
        /// <param name="processor">If null, the default <see cref="StreamFileProcessor"/>is used</param>
        /// <exception cref="ArgumentException">Thrown if: File Type is null, empty or whitespace; File Type is not a valid extension; Processor does not handle the file type.</exception>
        public FileConfiguration(string fileType, MultiViewConfiguration<FilePayload> view, IFileProcessor processor = null)
        {
            if (string.IsNullOrWhiteSpace(fileType))
            {
                throw new ArgumentException("File Type cannot be null, empty, or whitespace");
            }

            if (Path.GetExtension(fileType) != fileType && fileType != "*")
            {
                throw new ArgumentException("File Type is not valid");
            }

            if (fileType != processor.FileType && processor.FileType != "*")
            {
                throw new ArgumentException("Processor does not handle the file type");
            }

            FileType = fileType;
            View = view ?? throw new ArgumentNullException(nameof(view));
            Processor = processor ?? new StreamFileProcessor();
        }

        public string FileType { get; }

        public MultiViewConfiguration<FilePayload> View { get; }

        public IFileProcessor Processor { get; }
    }
}
