using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Platform.Data
{
    public interface IFileSavePayload
    {
        IReadOnlyList<string> FileTypes { get; }

        string SavePath { get; set; }
    }
}
