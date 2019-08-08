using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Models
{
    public class PickedFile
    {
        public PickedFile(string path, PickedFileSource source)
        {
            Path = path;
            Source = source;
        }

        public string Path { get; }

        public PickedFileSource Source { get; }
    }
}
