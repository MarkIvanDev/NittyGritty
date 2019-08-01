using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Platform.Files
{
    public class NGFile : ObservableObject
    {

        private string _path;

        public string Path
        {
            get { return _path; }
            set { Set(ref _path, value); }
        }

        private object _content;

        public object Content
        {
            get { return _content; }
            set { Set(ref _content, value); }
        }

    }
}
