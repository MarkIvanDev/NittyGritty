using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Shortcut;
using NittyGritty.Services.Core;

namespace NittyGritty.Services
{
    public partial class ShortcutService : IShortcutService
    {
        public async Task Create(ShortcutItem shortcut)
        {
            await PlatformCreate(shortcut);
        }

        public async Task Delete(string shortcutId)
        {
            await PlatformDelete(shortcutId);
        }

        public async Task Update(ShortcutItem shortcut)
        {
            await PlatformUpdate(shortcut);
        }

        public bool Exists(string shortcutId)
        {
            return PlatformExists(shortcutId);
        }

        public async Task<IReadOnlyList<ShortcutItem>> GetShortcuts()
        {
            return await PlatformGetShortcuts();
        }
    }
}
