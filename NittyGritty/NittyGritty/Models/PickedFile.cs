using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Models
{
    public class PickedFile : ObservableObject
    {
        private string _path;

        public string Path
        {
            get { return _path; }
            set { Set(ref _path, value); }
        }

        private PickedFileSource _source;

        public PickedFileSource Source
        {
            get { return _source; }
            set { Set(ref _source, value); }
        }

    }
}
