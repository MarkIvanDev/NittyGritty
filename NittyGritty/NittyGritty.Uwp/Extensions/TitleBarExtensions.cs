using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            return (Color?)obj.GetValue(BackgroundColorProperty);
        }

        public static void SetBackgroundColor(Page obj, Color? value)
        {
            obj.SetValue(BackgroundColorProperty, value);
        }

        // Using a DependencyProperty as the backing store for BackgroundColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackgroundColorProperty =
            DependencyProperty.RegisterAttached("BackgroundColor", typeof(Color?), typeof(TitleBarExtensions), new PropertyMetadata(null));



        public static Color? GetButtonBackgroundColor(Page obj)
        {
            return (Color?)obj.GetValue(ButtonBackgroundColorProperty);
        }

        public static void SetButtonBackgroundColor(Page obj, Color? value)
        {
            obj.SetValue(ButtonBackgroundColorProperty, value);
        }

        // Using a DependencyProperty as the backing store for ButtonBackgroundColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonBackgroundColorProperty =
            DependencyProperty.RegisterAttached("ButtonBackgroundColor", typeof(Color?), typeof(TitleBarExtensions), new PropertyMetadata(null));



        public static Color? GetButtonForegroundColor(Page obj)
        {
            return (Color?)obj.GetValue(ButtonForegroundColorProperty);
        }

        public static void SetButtonForegroundColor(Page obj, Color? value)
        {
            obj.SetValue(ButtonForegroundColorProperty, value);
        }

        // Using a DependencyProperty as the backing store for ButtonForegroundColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonForegroundColorProperty =
            DependencyProperty.RegisterAttached("ButtonForegroundColor", typeof(Color?), typeof(TitleBarExtensions), new PropertyMetadata(null));



        public static Color? GetButtonHoverBackgroundColor(Page obj)
        {
            return (Color?)obj.GetValue(ButtonHoverBackgroundColorProperty);
        }

        public static void SetButtonHoverBackgroundColor(Page obj, Color? value)
        {
            obj.SetValue(ButtonHoverBackgroundColorProperty, value);
        }

        // Using a DependencyProperty as the backing store for ButtonHoverBackgroundColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonHoverBackgroundColorProperty =
            DependencyProperty.RegisterAttached("ButtonHoverBackgroundColor", typeof(Color?), typeof(TitleBarExtensions), new PropertyMetadata(null));



        public static Color? GetButtonHoverForegroundColor(Page obj)
        {
            return (Color?)obj.GetValue(ButtonHoverForegroundColorProperty);
        }

        public static void SetButtonHoverForegroundColor(Page obj, Color? value)
        {
            obj.SetValue(ButtonHoverForegroundColorProperty, value);
        }

        // Using a DependencyProperty as the backing store for ButtonHoverForegroundColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonHoverForegroundColorProperty =
            DependencyProperty.RegisterAttached("ButtonHoverForegroundColor", typeof(Color?), typeof(TitleBarExtensions), new PropertyMetadata(null));



        public static Color? GetButtonInactiveBackgroundColor(DependencyObject obj)
        {
            return (Color?)obj.GetValue(ButtonInactiveBackgroundColorProperty);
        }

        public static void SetButtonInactiveBackgroundColor(DependencyObject obj, Color? value)
        {
            obj.SetValue(ButtonInactiveBackgroundColorProperty, value);
        }

        // Using a DependencyProperty as the backing store for ButtonInactiveBackgroundColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonInactiveBackgroundColorProperty =
            DependencyProperty.RegisterAttached("ButtonInactiveBackgroundColor", typeof(Color?), typeof(TitleBarExtensions), new PropertyMetadata(null));



        public static Color? GetButtonInactiveForegroundColor(Page obj)
        {
            return (Color?)obj.GetValue(ButtonInactiveForegroundColorProperty);
        }

        public static void SetButtonInactiveForegroundColor(Page obj, Color? value)
        {
            obj.SetValue(ButtonInactiveForegroundColorProperty, value);
        }

        // Using a DependencyProperty as the backing store for ButtonInactiveForegroundColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonInactiveForegroundColorProperty =
            DependencyProperty.RegisterAttached("ButtonInactiveForegroundColor", typeof(Color?), typeof(TitleBarExtensions), new PropertyMetadata(null));



        public static Color? GetButtonPressedBackgroundColor(Page obj)
        {
            return (Color?)obj.GetValue(ButtonPressedBackgroundColorProperty);
        }

        public static void SetButtonPressedBackgroundColor(Page obj, Color? value)
        {
            obj.SetValue(ButtonPressedBackgroundColorProperty, value);
        }

        // Using a DependencyProperty as the backing store for ButtonPressedBackgroundColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonPressedBackgroundColorProperty =
            DependencyProperty.RegisterAttached("ButtonPressedBackgroundColor", typeof(Color?), typeof(TitleBarExtensions), new PropertyMetadata(null));



        public static Color? GetButtonPressedForegroundColor(Page obj)
        {
            return (Color?)obj.GetValue(ButtonPressedForegroundColorProperty);
        }

        public static void SetButtonPressedForegroundColor(Page obj, Color? value)
        {
            obj.SetValue(ButtonPressedForegroundColorProperty, value);
        }

        // Using a DependencyProperty as the backing store for ButtonPressedForegroundColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonPressedForegroundColorProperty =
            DependencyProperty.RegisterAttached("ButtonPressedForegroundColor", typeof(Color?), typeof(TitleBarExtensions), new PropertyMetadata(null));



        public static Color? GetForegroundColor(Page obj)
        {
            return (Color?)obj.GetValue(ForegroundColorProperty);
        }

        public static void SetForegroundColor(Page obj, Color? value)
        {
            obj.SetValue(ForegroundColorProperty, value);
        }

        // Using a DependencyProperty as the backing store for ForegroundColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ForegroundColorProperty =
            DependencyProperty.RegisterAttached("ForegroundColor", typeof(Color?), typeof(TitleBarExtensions), new PropertyMetadata(null));



        public static Color? GetInactiveBackgroundColor(Page obj)
        {
            return (Color?)obj.GetValue(InactiveBackgroundColorProperty);
        }

        public static void SetInactiveBackgroundColor(Page obj, Color? value)
        {
            obj.SetValue(InactiveBackgroundColorProperty, value);
        }

        // Using a DependencyProperty as the backing store for InactiveBackgroundColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InactiveBackgroundColorProperty =
            DependencyProperty.RegisterAttached("InactiveBackgroundColor", typeof(Color?), typeof(TitleBarExtensions), new PropertyMetadata(null));



        public static Color? GetInactiveForegroundColor(Page obj)
        {
            return (Color?)obj.GetValue(InactiveForegroundColorProperty);
        }

        public static void SetInactiveForegroundColor(Page obj, Color? value)
        {
            obj.SetValue(InactiveForegroundColorProperty, value);
        }

        // Using a DependencyProperty as the backing store for InactiveForegroundColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InactiveForegroundColorProperty =
            DependencyProperty.RegisterAttached("InactiveForegroundColor", typeof(Color?), typeof(TitleBarExtensions), new PropertyMetadata(null));



    }
}
