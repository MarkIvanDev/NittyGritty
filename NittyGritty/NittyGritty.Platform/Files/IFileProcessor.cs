using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.Platform.Files
{
    public interface IFileProcessor
    {
        string FileType { get; }

        Task<object> Process(object file);
    }
    
}
