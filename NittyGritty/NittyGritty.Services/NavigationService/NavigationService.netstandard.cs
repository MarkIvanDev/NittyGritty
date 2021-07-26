using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Services
{
    public partial class NavigationService
    {
        bool PlatformCanGoBack { get { throw new NotImplementedException(); } }

        bool PlatformCanGoForward { get { throw new NotImplementedException(); } }

        string PlatformCurrentKey { get { throw new NotImplementedException(); } }

        void PlatformGoBack()
            => throw new NotImplementedException();

        void PlatformGoForward()
            => throw new NotImplementedException();

        void PlatformNavigateTo(string key)
            => throw new NotImplementedException();

        void PlatformNavigateTo(string key, object parameter)
            => throw new NotImplementedException();
    }
}
