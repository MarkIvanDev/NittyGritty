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



        public static Color GetBackgroundColor(Page obj)
        {
            return GetTitleBar()?.BackgroundColor ?? default;
        }

        public static void SetBackgroundColor(Page obj, Color value)
        {
            var titleBar = GetTitleBar();
            if (!(titleBar is null))
            {
                titleBar.BackgroundColor = value;
            }
        }



        public static Color GetButtonBackgroundColor(Page obj)
        {
            return GetTitleBar()?.ButtonBackgroundColor ?? default;
        }

        public static void SetButtonBackgroundColor(Page obj, Color value)
        {
            var titleBar = GetTitleBar();
            if (!(titleBar is null))
            {
                titleBar.ButtonBackgroundColor = value;
            }
        }



        public static Color GetButtonForegroundColor(Page obj)
        {
            return GetTitleBar()?.ButtonForegroundColor ?? default;
        }

        public static void SetButtonForegroundColor(Page obj, Color value)
        {
            var titleBar = GetTitleBar();
            if (!(titleBar is null))
            {
                titleBar.ButtonForegroundColor = value;
            }
        }



        public static Color GetButtonHoverBackgroundColor(Page obj)
        {
            return GetTitleBar()?.ButtonHoverBackgroundColor ?? default;
        }

        public static void SetButtonHoverBackgroundColor(Page obj, Color value)
        {
            var titleBar = GetTitleBar();
            if (!(titleBar is null))
            {
                titleBar.ButtonHoverBackgroundColor = value;
            }
        }



        public static Color GetButtonHoverForegroundColor(Page obj)
        {
            return GetTitleBar()?.ButtonHoverForegroundColor ?? default;
        }

        public static void SetButtonHoverForegroundColor(Page obj, Color value)
        {
            var titleBar = GetTitleBar();
            if (!(titleBar is null))
            {
                titleBar.ButtonHoverForegroundColor = value;
            }
        }



        public static Color GetButtonInactiveBackgroundColor(Page obj)
        {
            return GetTitleBar()?.ButtonInactiveBackgroundColor ?? default;
        }

        public static void SetButtonInactiveBackgroundColor(Page obj, Color value)
        {
            var titleBar = GetTitleBar();
            if (!(titleBar is null))
            {
                titleBar.ButtonInactiveBackgroundColor = value;
            }
        }



        public static Color GetButtonInactiveForegroundColor(Page obj)
        {
            return GetTitleBar()?.ButtonInactiveForegroundColor ?? default;
        }

        public static void SetButtonInactiveForegroundColor(Page obj, Color value)
        {
            var titleBar = GetTitleBar();
            if (!(titleBar is null))
            {
                titleBar.ButtonInactiveForegroundColor = value;
            }
        }



        public static Color GetButtonPressedBackgroundColor(Page obj)
        {
            return GetTitleBar()?.ButtonPressedBackgroundColor ?? default;
        }

        public static void SetButtonPressedBackgroundColor(Page obj, Color value)
        {
            var titleBar = GetTitleBar();
            if (!(titleBar is null))
            {
                titleBar.ButtonPressedBackgroundColor = value;
            }
        }



        public static Color GetButtonPressedForegroundColor(Page obj)
        {
            return GetTitleBar()?.ButtonPressedForegroundColor ?? default;
        }

        public static void SetButtonPressedForegroundColor(Page obj, Color value)
        {
            var titleBar = GetTitleBar();
            if (!(titleBar is null))
            {
                titleBar.ButtonPressedForegroundColor = value;
            }
        }



        public static Color GetForegroundColor(Page obj)
        {
            return GetTitleBar()?.ForegroundColor ?? default;
        }

        public static void SetForegroundColor(Page obj, Color value)
        {
            var titleBar = GetTitleBar();
            if (!(titleBar is null))
            {
                titleBar.ForegroundColor = value;
            }
        }



        public static Color GetInactiveBackgroundColor(Page obj)
        {
            return GetTitleBar()?.InactiveBackgroundColor ?? default;
        }

        public static void SetInactiveBackgroundColor(Page obj, Color value)
        {
            var titleBar = GetTitleBar();
            if (!(titleBar is null))
            {
                titleBar.InactiveBackgroundColor = value;
            }
        }



        public static Color GetInactiveForegroundColor(Page obj)
        {
            return GetTitleBar()?.InactiveForegroundColor ?? default;
        }

        public static void SetInactiveForegroundColor(Page obj, Color value)
        {
            var titleBar = GetTitleBar();
            if (!(titleBar is null))
            {
                titleBar.InactiveForegroundColor = value;
            }
        }



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



    }
}
