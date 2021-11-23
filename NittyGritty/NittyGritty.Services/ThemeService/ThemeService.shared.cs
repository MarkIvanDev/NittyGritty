﻿using System;
using System.Collections.Generic;
using System.Text;
using NittyGritty.Platform.Theme;
using NittyGritty.Services.Core;

namespace NittyGritty.Services
{
    public partial class ThemeService : IThemeService
    {
        public AppTheme GetTheme()
            => PlatformGetTheme();

        public void SetTheme(AppTheme theme)
            => PlatformSetTheme(theme);

    }
}
