﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Services.Core
{
    public interface INavigationService
    {
        string CurrentKey { get; }

        bool CanGoBack { get; }

        bool CanGoForward { get; }

        void GoBack();

        void GoForward();

        void NavigateTo(string key);

        void NavigateTo(string key, object parameter);

        void ClearBackStack();

        void ClearForwardStack();
    }
}
