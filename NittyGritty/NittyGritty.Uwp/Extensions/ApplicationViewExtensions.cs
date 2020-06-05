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

        // Using a DependencyProperty as the backing store for FullScreenSystemOverlayMode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FullScreenSystemOverlayModeProperty =
            DependencyProperty.RegisterAttached("FullScreenSystemOverlayMode", typeof(FullScreenSystemOverlayMode), typeof(ApplicationViewExtensions), new PropertyMetadata(default(FullScreenSystemOverlayMode)));



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

        // Using a DependencyProperty as the backing store for IsScreenCaptureEnabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsScreenCaptureEnabledProperty =
            DependencyProperty.RegisterAttached("IsScreenCaptureEnabled", typeof(bool), typeof(ApplicationViewExtensions), new PropertyMetadata(default(bool)));



        public static Size GetPreferredLaunchViewSize(DependencyObject obj)
        {
            return ApplicationView.PreferredLaunchViewSize;
        }

        public static void SetPreferredLaunchViewSize(DependencyObject obj, Size value)
        {
            ApplicationView.PreferredLaunchViewSize = value;
        }

        // Using a DependencyProperty as the backing store for PreferredLaunchViewSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PreferredLaunchViewSizeProperty =
            DependencyProperty.RegisterAttached("PreferredLaunchViewSize", typeof(Size), typeof(ApplicationViewExtensions), new PropertyMetadata(default(Size)));



        public static ApplicationViewWindowingMode GetPreferredLaunchWindowingMode(DependencyObject obj)
        {
            return ApplicationView.PreferredLaunchWindowingMode;
        }

        public static void SetPreferredLaunchWindowingMode(DependencyObject obj, ApplicationViewWindowingMode  value)
        {
            ApplicationView.PreferredLaunchWindowingMode = value;
        }

        // Using a DependencyProperty as the backing store for PreferredLaunchWindowingMode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PreferredLaunchWindowingModeProperty =
            DependencyProperty.RegisterAttached("PreferredLaunchWindowingMode", typeof(ApplicationViewWindowingMode ), typeof(ApplicationViewExtensions), new PropertyMetadata(default(ApplicationViewWindowingMode)));



        public static bool GetTerminateAppOnFinalViewClose(DependencyObject obj)
        {
            return ApplicationView.TerminateAppOnFinalViewClose;
        }

        public static void SetTerminateAppOnFinalViewClose(DependencyObject obj, bool value)
        {
            ApplicationView.TerminateAppOnFinalViewClose = value;
        }

        // Using a DependencyProperty as the backing store for TerminateAppOnFinalViewClose.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TerminateAppOnFinalViewCloseProperty =
            DependencyProperty.RegisterAttached("TerminateAppOnFinalViewClose", typeof(bool), typeof(ApplicationViewExtensions), new PropertyMetadata(default(bool)));



        public static string GetTitle(DependencyObject obj)
        {
            return GetApplicationView()?.Title ?? string.Empty;
        }

        public static void SetTitle(DependencyObject obj, string value)
        {
            var applicationView = GetApplicationView();
            if (!(applicationView is null))
            {
                applicationView.Title = value ?? string.Empty;
            }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.RegisterAttached("Title", typeof(string), typeof(ApplicationViewExtensions), new PropertyMetadata(string.Empty));


    }
}
