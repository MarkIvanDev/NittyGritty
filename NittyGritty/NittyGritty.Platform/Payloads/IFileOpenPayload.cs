using NittyGritty.Models;
using NittyGritty.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.Platform.Payloads
{
    public interface IFileOpenPayload
    {
        FilePickerSettings Settings { get; }

        Task Add(PickedFile file);

        Task Add(IEnumerable<PickedFile> files);

        Task Remove(PickedFile file);

        Task Remove(IEnumerable<PickedFile> files);
    }
}
