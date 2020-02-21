using NittyGritty.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.Platform.Payloads
{
    public interface IFileOpenPayload
    {
        bool IsMultiSelect { get; }

        IReadOnlyList<string> FileTypes { get; }

        Task Add(PickedFile file);

        Task Add(IEnumerable<PickedFile> files);

        Task Remove(PickedFile file);

        Task Remove(IEnumerable<PickedFile> files);
    }
}
