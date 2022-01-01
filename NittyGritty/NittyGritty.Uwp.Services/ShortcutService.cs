using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Shortcut;
using NittyGritty.Services.Core;
using Windows.UI.StartScreen;

namespace NittyGritty.Uwp.Services
{
    public class ShortcutService : IShortcutService
    {
        public async Task Create(ShortcutItem shortcut)
        {
            var tile = ToPlatformShortcut(shortcut);
            await tile.RequestCreateAsync();
        }

        public async Task Delete(string shortcutId)
        {
            var tile = new SecondaryTile(shortcutId);
            await tile.RequestDeleteAsync();
        }

        public async Task Update(ShortcutItem shortcut)
        {
            var tile = ToPlatformShortcut(shortcut);
            await tile.UpdateAsync();
        }

        SecondaryTile ToPlatformShortcut(ShortcutItem shortcut)
        {
            var tile = new SecondaryTile(
                tileId: shortcut.Id,
                displayName: shortcut.DisplayName,
                arguments: shortcut.Arguments,
                square150x150Logo: shortcut.Icon,
                desiredSize: TileSize.Default
            );
            tile.VisualElements.ShowNameOnSquare150x150Logo = true;

            if (shortcut.SmallIcon != null)
            {
                tile.VisualElements.Square71x71Logo = shortcut.SmallIcon;
            }

            if (shortcut.WideIcon != null)
            {
                tile.VisualElements.Wide310x150Logo = shortcut.WideIcon;
                tile.VisualElements.ShowNameOnWide310x150Logo = true;
            }

            if (shortcut.LargeIcon != null)
            {
                tile.VisualElements.Square310x310Logo = shortcut.LargeIcon;
                tile.VisualElements.ShowNameOnSquare310x310Logo = true;
            }

            return tile;
        }

        public bool Exists(string shortcutId)
        {
            return SecondaryTile.Exists(shortcutId);
        }

        public async Task<IList<ShortcutItem>> GetShortcuts()
        {
            var items = new List<ShortcutItem>();

            var tiles = await SecondaryTile.FindAllAsync();
            foreach (var tile in tiles)
            {
                items.Add(new ShortcutItem()
                {
                    Id = tile.TileId,
                    DisplayName = tile.DisplayName,
                    Arguments = tile.Arguments,
                    Icon = tile.VisualElements.Square150x150Logo,
                    SmallIcon = tile.VisualElements.Square71x71Logo,
                    WideIcon = tile.VisualElements.Wide310x150Logo,
                    LargeIcon = tile.VisualElements.Square310x310Logo
                });
            }

            return items;
        }
    }
}
