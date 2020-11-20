using System;
using System.Collections.Generic;
using System.Text;
using NittyGritty.Platform.Theme;

namespace NittyGritty.Services
{
    public interface IThemeService
    {
        void SetTheme(AppTheme theme);

        AppTheme GetTheme();

    }
}
