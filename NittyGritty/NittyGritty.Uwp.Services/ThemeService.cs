using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NittyGritty.Platform.Theme;
using NittyGritty.Services.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace NittyGritty.Uwp.Services
{
    public class ThemeService : IThemeService
    {
        public void SetTheme(AppTheme theme)
        {
            if (Window.Current.Content is FrameworkElement content)
            {
                content.RequestedTheme = (ElementTheme)theme;
                var popups = VisualTreeHelper.GetOpenPopups(Window.Current);
                foreach (var item in popups)
                {
                    item.IsOpen = false;
                }
            }
        }

        public AppTheme GetTheme()
        {
            return Window.Current.Content is FrameworkElement content ?
                (AppTheme)content.RequestedTheme : AppTheme.Default;
        }
    }
}
