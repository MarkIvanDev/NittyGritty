using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NittyGritty.Platform.Files
{
    public class NGFile : ObservableObject
    {
        public NGFile(string path, Stream content)
        {
            Path = path;
            Content = content;
        }

        public string Path { get; }

        public Stream Content { get; }
    }
}
