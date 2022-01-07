using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Shortcut;

namespace NittyGritty.Services.Core
{
    public interface IShortcutService
    {
        Task Create(ShortcutItem shortcut);

        Task Delete(string shortcutId);

        Task Update(ShortcutItem shortcut);

        bool Exists(string shortcutId);

        Task<IList<ShortcutItem>> GetShortcuts();

    }
}
