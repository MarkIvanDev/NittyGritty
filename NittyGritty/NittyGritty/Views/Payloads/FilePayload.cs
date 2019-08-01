using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NittyGritty.Views.Payloads
{
    internal class FilePayload
    {
        public FilePayload(string action, string fileType, IList<object> data)
        {
            Action = action;
            FileType = fileType;
            Data = new ReadOnlyCollection<object>(data);
        }

        public string Action { get; }

        public string FileType { get; }

        public ReadOnlyCollection<object> Data { get; }
    }
}
