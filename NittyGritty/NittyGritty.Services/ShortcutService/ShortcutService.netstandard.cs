using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Shortcut;

namespace NittyGritty.Services
{
    public partial class ShortcutService
    {
        Task PlatformCreate(ShortcutItem shortcut)
            => throw new NotImplementedException();

        Task PlatformDelete(string shortcutId)
            => throw new NotImplementedException();

        Task PlatformUpdate(ShortcutItem shortcut)
            => throw new NotImplementedException();

        bool PlatformExists(string shortcutId)
            => throw new NotImplementedException();

        Task<IReadOnlyList<ShortcutItem>> PlatformGetShortcuts()
            => throw new NotImplementedException();
    }
}
