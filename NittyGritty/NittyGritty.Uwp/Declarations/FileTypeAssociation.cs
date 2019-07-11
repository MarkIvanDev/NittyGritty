using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace NittyGritty.Uwp.Declarations
{
    public abstract class FileTypeAssociation
    {
        public FileTypeAssociation(string fileType)
        {
            FileType = fileType;
        }

        public string FileType { get; }

        public abstract Task Run(IStorageFile file);
    }
}
