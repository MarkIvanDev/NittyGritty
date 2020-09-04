using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp.Extensions
{
    public static class ApplicationViewExtensions
    {
        public static bool IsSupported => ApiInformation.IsTypePresent("Windows.UI.ViewManagement.ApplicationView");

        private static ApplicationView GetApplicationView()
        {
            return IsSupported ? ApplicationView.GetForCurrentView() : null;
        }



        public static FullScreenSystemOverlayMode GetFullScreenSystemOverlayMode(Page obj)
        {
            return GetApplicationView()?.FullScreenSystemOverlayMode ?? default;
        }

        public static void SetFullScreenSystemOverlayMode(Page obj, FullScreenSystemOverlayMode value)
        {
            var applicationView = GetApplicationView();
            if (!(applicationView is null))
            {
                applicationView.FullScreenSystemOverlayMode = value;
            }
        }



        public static bool GetIsScreenCaptureEnabled(Page obj)
        {
            return GetApplicationView()?.IsScreenCaptureEnabled ?? default;
        }

        public static void SetIsScreenCaptureEnabled(Page obj, bool value)
        {
            var applicationView = GetApplicationView();
            if (!(applicationView is null))
            {
                applicationView.IsScreenCaptureEnabled = value;
            }
        }



        public static Size GetPreferredLaunchViewSize(Page obj)
        {
            return ApplicationView.PreferredLaunchViewSize;
        }

        public static void SetPreferredLaunchViewSize(Page obj, Size value)
        {
            ApplicationView.PreferredLaunchViewSize = value;
        }



        public static ApplicationViewWindowingMode GetPreferredLaunchWindowingMode(Page obj)
        {
            return ApplicationView.PreferredLaunchWindowingMode;
        }

        public static void SetPreferredLaunchWindowingMode(Page obj, ApplicationViewWindowingMode  value)
        {
            ApplicationView.PreferredLaunchWindowingMode = value;
        }



        public static bool GetTerminateAppOnFinalViewClose(Page obj)
        {
            return ApplicationView.TerminateAppOnFinalViewClose;
        }

        public static void SetTerminateAppOnFinalViewClose(Page obj, bool value)
        {
            ApplicationView.TerminateAppOnFinalViewClose = value;
        }



        public static string GetTitle(Page obj)
        {
            return GetApplicationView()?.Title ?? string.Empty;
        }

        public static void SetTitle(Page obj, string value)
        {
            var applicationView = GetApplicationView();
            if (!(applicationView is null))
            {
                applicationView.Title = value ?? string.Empty;
            }
        }


    }
}
