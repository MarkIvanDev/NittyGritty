using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Services
{
    public partial class NavigationService
    {
        public bool PlatformCanGoBack { get { throw new NotImplementedException(); } }

        public bool PlatformCanGoForward { get { throw new NotImplementedException(); } }

        public string PlatformCurrentKey { get { throw new NotImplementedException(); } }

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
