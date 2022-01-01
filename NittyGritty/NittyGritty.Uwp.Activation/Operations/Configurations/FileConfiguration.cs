using System;
using System.IO;
using System.Text;
using NittyGritty.Platform.Payloads;

namespace NittyGritty.Uwp.Activation.Operations.Configurations
{
    public class FileConfiguration
    {
        /// <summary>
        /// Creates a configuration for the FileOperation
        /// </summary>
        /// <param name="fileType">The file type that this configuration is for.
        /// A file type with a value of * will be used as fallback for unknown file types</param>
        /// <param name="view"></param>
        /// <exception cref="ArgumentException">Thrown if: File Type is null, empty or whitespace; File Type is not a valid extension; Processor does not handle the file type.</exception>
        public FileConfiguration(string fileType, MultiViewConfiguration<FilePayload> view)
        {
            if (string.IsNullOrWhiteSpace(fileType))
            {
                throw new ArgumentException("File Type cannot be null, empty, or whitespace");
            }

            if (Path.GetExtension(fileType) != fileType && fileType != "*")
            {
                throw new ArgumentException("File Type is not valid");
            }

            FileType = fileType;
            View = view ?? throw new ArgumentNullException(nameof(view));
        }

        public string FileType { get; }

        public MultiViewConfiguration<FilePayload> View { get; }
    }
}
