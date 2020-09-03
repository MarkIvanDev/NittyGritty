using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Shortcut;

namespace NittyGritty.Services
{
    public interface IShortcutService
    {
        Task Create(ShortcutItem shortcut);

        Task Delete(string shortcutId);

        Task Update(ShortcutItem shortcut);

        bool Exists(string shortcutId);

        Task<IReadOnlyList<ShortcutItem>> GetShortcuts();

    }
}
