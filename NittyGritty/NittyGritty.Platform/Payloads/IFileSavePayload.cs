using NittyGritty.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Platform.Payloads
{
    public interface IFileSavePayload
    {
        FilePickerSettings Settings { get; }

        string SavePath { get; set; }
    }
}
