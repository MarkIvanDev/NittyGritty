using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Views
{
    public class FilePickerSettings
    {
        public FilePickerSettings(bool isMultiSelect, IReadOnlyList<string> fileTypes)
        {
            IsMultiSelect = isMultiSelect;
            FileTypes = fileTypes;
        }

        public bool IsMultiSelect { get; }

        public IReadOnlyList<string> FileTypes { get; }
    }
}
