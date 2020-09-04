using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Shortcut;
using Windows.UI.StartScreen;

namespace NittyGritty.Services
{
    public partial class ShortcutService
    {

        async Task PlatformCreate(ShortcutItem shortcut)
        {
            var tile = ToPlatformShortcut(shortcut);
            await tile.RequestCreateAsync();
        }

        async Task PlatformDelete(string shortcutId)
        {
            var tile = new SecondaryTile(shortcutId);
            await tile.RequestDeleteAsync();
        }

        async Task PlatformUpdate(ShortcutItem shortcut)
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

        bool PlatformExists(string shortcutId)
        {
            return SecondaryTile.Exists(shortcutId);
        }

        async Task<IReadOnlyList<ShortcutItem>> PlatformGetShortcuts()
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

            return new ReadOnlyCollection<ShortcutItem>(items);
        }

    }
}
