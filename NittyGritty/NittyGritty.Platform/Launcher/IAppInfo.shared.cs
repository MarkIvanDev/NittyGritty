using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.Platform.Launcher
{
    public interface IAppInfo
    {
        string Id { get; }

        string DisplayName { get; }

        string PackageFamilyName { get; }

        string Description { get; }

        string AppUserModelId { get; }

        Task<Stream> GetLogo(Size size);
    }
}
