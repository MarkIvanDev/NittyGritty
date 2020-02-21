using NittyGritty.Models;
using NittyGritty.Platform.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.Platform.Data
{
    public interface ICommandLinePayload
    {
        string OriginalArguments { get; }

        IFolder CurrentDirectory { get; }

        string Command { get; }

        QueryString Parameters { get; }
    }
}
