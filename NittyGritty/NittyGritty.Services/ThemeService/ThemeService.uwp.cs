using System;
using System.Collections.Generic;
using System.Text;
using NittyGritty.Platform.Theme;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace NittyGritty.Services
{
    public partial class ThemeService
    {
        void PlatformSetTheme(AppTheme theme)
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

        AppTheme PlatformGetTheme()
        {
            return Window.Current.Content is FrameworkElement content ?
                (AppTheme)content.RequestedTheme : AppTheme.Default;
        }
    }
}
