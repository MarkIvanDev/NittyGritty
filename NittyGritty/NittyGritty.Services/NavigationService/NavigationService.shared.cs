using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Services
{
    public partial class NavigationService : INavigationService
    {
        private const string RootKey = "-- ROOT --";
        private const string UnknownKey = "-- UNKNOWN -- ";
        private const string OOBKey = "-- OOB --";

        public string CurrentKey { get { return PlatformCurrentKey; } }

        public bool CanGoBack { get { return PlatformCanGoBack; } }

        public bool CanGoForward { get { return PlatformCanGoForward; } }

        public void GoBack()
        {
            PlatformGoBack();
        }

        public void GoForward()
        {
            PlatformGoForward();
        }

        public void NavigateTo(string key)
        {
            PlatformNavigateTo(key);
        }

        public void NavigateTo(string key, object parameter)
        {
            PlatformNavigateTo(key, parameter);
        }
    }
}
