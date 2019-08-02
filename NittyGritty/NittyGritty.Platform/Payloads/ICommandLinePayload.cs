using NittyGritty.Models;
using NittyGritty.Platform.Files;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.Platform.Payloads
{
    public interface ICommandLinePayload
    {
        string OriginalArguments { get; }

        string CurrentDirectory { get; }

        string Command { get; }

        QueryString Parameters { get; }

        Task<IReadOnlyCollection<NGFile>> GetFiles();
    }
}
