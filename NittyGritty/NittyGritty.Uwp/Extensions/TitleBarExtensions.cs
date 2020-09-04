using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp.Extensions
{
    public static class TitleBarExtensions
    {
        public static bool IsSupported => ApiInformation.IsTypePresent("Windows.UI.ViewManagement.ApplicationViewTitleBar");

        private static ApplicationViewTitleBar GetTitleBar()
        {
            return IsSupported ? ApplicationView.GetForCurrentView().TitleBar : null;
        }



        public static Color? GetBackgroundColor(Page obj)
        {
            return GetTitleBar()?.BackgroundColor;
        }

        public static void SetBackgroundColor(Page obj, Color? value)
        {
            var titleBar = GetTitleBar();
            if (!(titleBar is null))
            {
                titleBar.BackgroundColor = value;
            }
        }

        // Using a DependencyProperty as the backing store for BackgroundColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackgroundColorProperty =
            DependencyProperty.RegisterAttached("BackgroundColor", typeof(Color?), typeof(TitleBarExtensions), new PropertyMetadata(null));



        public static Color? GetButtonBackgroundColor(Page obj)
        {
            return GetTitleBar()?.ButtonBackgroundColor;
        }

        public static void SetButtonBackgroundColor(Page obj, Color? value)
        {
            var titleBar = GetTitleBar();
            if (!(titleBar is null))
            {
                titleBar.ButtonBackgroundColor = value;
            }
        }

        // Using a DependencyProperty as the backing store for ButtonBackgroundColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonBackgroundColorProperty =
            DependencyProperty.RegisterAttached("ButtonBackgroundColor", typeof(Color?), typeof(TitleBarExtensions), new PropertyMetadata(null));



        public static Color? GetButtonForegroundColor(Page obj)
        {
            return GetTitleBar()?.ButtonForegroundColor;
        }

        public static void SetButtonForegroundColor(Page obj, Color? value)
        {
            var titleBar = GetTitleBar();
            if (!(titleBar is null))
            {
                titleBar.ButtonForegroundColor = value;
            }
        }

        // Using a DependencyProperty as the backing store for ButtonForegroundColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonForegroundColorProperty =
            DependencyProperty.RegisterAttached("ButtonForegroundColor", typeof(Color?), typeof(TitleBarExtensions), new PropertyMetadata(null));



        public static Color? GetButtonHoverBackgroundColor(Page obj)
        {
            return GetTitleBar()?.ButtonHoverBackgroundColor;
        }

        public static void SetButtonHoverBackgroundColor(Page obj, Color? value)
        {
            var titleBar = GetTitleBar();
            if (!(titleBar is null))
            {
                titleBar.ButtonHoverBackgroundColor = value;
            }
        }

        // Using a DependencyProperty as the backing store for ButtonHoverBackgroundColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonHoverBackgroundColorProperty =
            DependencyProperty.RegisterAttached("ButtonHoverBackgroundColor", typeof(Color?), typeof(TitleBarExtensions), new PropertyMetadata(null));



        public static Color? GetButtonHoverForegroundColor(Page obj)
        {
            return GetTitleBar()?.ButtonHoverForegroundColor;
        }

        public static void SetButtonHoverForegroundColor(Page obj, Color? value)
        {
            var titleBar = GetTitleBar();
            if (!(titleBar is null))
            {
                titleBar.ButtonHoverForegroundColor = value;
            }
        }

        // Using a DependencyProperty as the backing store for ButtonHoverForegroundColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonHoverForegroundColorProperty =
            DependencyProperty.RegisterAttached("ButtonHoverForegroundColor", typeof(Color?), typeof(TitleBarExtensions), new PropertyMetadata(null));



        public static Color? GetButtonInactiveBackgroundColor(Page obj)
        {
            return GetTitleBar()?.ButtonInactiveBackgroundColor;
        }

        public static void SetButtonInactiveBackgroundColor(Page obj, Color? value)
        {
            var titleBar = GetTitleBar();
            if (!(titleBar is null))
            {
                titleBar.ButtonInactiveBackgroundColor = value;
            }
        }

        // Using a DependencyProperty as the backing store for ButtonInactiveBackgroundColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonInactiveBackgroundColorProperty =
            DependencyProperty.RegisterAttached("ButtonInactiveBackgroundColor", typeof(Color?), typeof(TitleBarExtensions), new PropertyMetadata(null));



        public static Color? GetButtonInactiveForegroundColor(Page obj)
        {
            return GetTitleBar()?.ButtonInactiveForegroundColor;
        }

        public static void SetButtonInactiveForegroundColor(Page obj, Color? value)
        {
            var titleBar = GetTitleBar();
            if (!(titleBar is null))
            {
                titleBar.ButtonInactiveForegroundColor = value;
            }
        }

        // Using a DependencyProperty as the backing store for ButtonInactiveForegroundColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonInactiveForegroundColorProperty =
            DependencyProperty.RegisterAttached("ButtonInactiveForegroundColor", typeof(Color?), typeof(TitleBarExtensions), new PropertyMetadata(null));



        public static Color? GetButtonPressedBackgroundColor(Page obj)
        {
            return GetTitleBar()?.ButtonPressedBackgroundColor;
        }

        public static void SetButtonPressedBackgroundColor(Page obj, Color? value)
        {
            var titleBar = GetTitleBar();
            if (!(titleBar is null))
            {
                titleBar.ButtonPressedBackgroundColor = value;
            }
        }

        // Using a DependencyProperty as the backing store for ButtonPressedBackgroundColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonPressedBackgroundColorProperty =
            DependencyProperty.RegisterAttached("ButtonPressedBackgroundColor", typeof(Color?), typeof(TitleBarExtensions), new PropertyMetadata(null));



        public static Color? GetButtonPressedForegroundColor(Page obj)
        {
            return GetTitleBar()?.ButtonPressedForegroundColor;
        }

        public static void SetButtonPressedForegroundColor(Page obj, Color? value)
        {
            var titleBar = GetTitleBar();
            if (!(titleBar is null))
            {
                titleBar.ButtonPressedForegroundColor = value;
            }
        }

        // Using a DependencyProperty as the backing store for ButtonPressedForegroundColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonPressedForegroundColorProperty =
            DependencyProperty.RegisterAttached("ButtonPressedForegroundColor", typeof(Color?), typeof(TitleBarExtensions), new PropertyMetadata(null));



        public static Color? GetForegroundColor(Page obj)
        {
            return GetTitleBar()?.ForegroundColor;
        }

        public static void SetForegroundColor(Page obj, Color? value)
        {
            var titleBar = GetTitleBar();
            if (!(titleBar is null))
            {
                titleBar.ForegroundColor = value;
            }
        }

        // Using a DependencyProperty as the backing store for ForegroundColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ForegroundColorProperty =
            DependencyProperty.RegisterAttached("ForegroundColor", typeof(Color?), typeof(TitleBarExtensions), new PropertyMetadata(null));



        public static Color? GetInactiveBackgroundColor(Page obj)
        {
            return GetTitleBar()?.InactiveBackgroundColor;
        }

        public static void SetInactiveBackgroundColor(Page obj, Color? value)
        {
            var titleBar = GetTitleBar();
            if (!(titleBar is null))
            {
                titleBar.InactiveBackgroundColor = value;
            }
        }

        // Using a DependencyProperty as the backing store for InactiveBackgroundColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InactiveBackgroundColorProperty =
            DependencyProperty.RegisterAttached("InactiveBackgroundColor", typeof(Color?), typeof(TitleBarExtensions), new PropertyMetadata(null));



        public static Color? GetInactiveForegroundColor(Page obj)
        {
            return GetTitleBar()?.InactiveForegroundColor;
        }

        public static void SetInactiveForegroundColor(Page obj, Color? value)
        {
            var titleBar = GetTitleBar();
            if (!(titleBar is null))
            {
                titleBar.InactiveForegroundColor = value;
            }
        }

        // Using a DependencyProperty as the backing store for InactiveForegroundColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InactiveForegroundColorProperty =
            DependencyProperty.RegisterAttached("InactiveForegroundColor", typeof(Color?), typeof(TitleBarExtensions), new PropertyMetadata(null));



        private static CoreApplicationViewTitleBar GetCoreTitleBar()
        {
            return ApiInformation.IsTypePresent("Windows.ApplicationModel.Core.CoreApplicationViewTitleBar") ?
                CoreApplication.GetCurrentView().TitleBar :
                null;
        }



        public static bool GetExtendViewIntoTitleBar(Page obj)
        {
            return GetCoreTitleBar()?.ExtendViewIntoTitleBar ?? default;
        }

        public static void SetExtendViewIntoTitleBar(Page obj, bool value)
        {
            var titleBar = GetCoreTitleBar();
            if (!(titleBar is null))
            {
                titleBar.ExtendViewIntoTitleBar = value;
            }
        }

        // Using a DependencyProperty as the backing store for ExtendViewIntoTitleBar.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExtendViewIntoTitleBarProperty =
            DependencyProperty.RegisterAttached("ExtendViewIntoTitleBar", typeof(bool), typeof(TitleBarExtensions), new PropertyMetadata(default(bool)));



    }
}
