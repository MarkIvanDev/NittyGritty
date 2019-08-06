using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace NittyGritty.UI.Extensions
{
    public static class NavigationViewExtensions
    {
        public static string GetKey(DependencyObject obj)
        {
            return (string)obj.GetValue(KeyProperty);
        }

        public static void SetKey(DependencyObject obj, string value)
        {
            obj.SetValue(KeyProperty, value);
        }

        // Using a DependencyProperty as the backing store for Key.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty KeyProperty =
            DependencyProperty.RegisterAttached("Key", typeof(string), typeof(NavigationViewExtensions), new PropertyMetadata(null));


        public static object GetParameter(DependencyObject obj)
        {
            return (object)obj.GetValue(ParameterProperty);
        }

        public static void SetParameter(DependencyObject obj, object value)
        {
            obj.SetValue(ParameterProperty, value);
        }

        // Using a DependencyProperty as the backing store for Parameter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ParameterProperty =
            DependencyProperty.RegisterAttached("Parameter", typeof(object), typeof(NavigationViewExtensions), new PropertyMetadata(null));


        public static string GetSettingsKey(DependencyObject obj)
        {
            return (string)obj.GetValue(SettingsKeyProperty);
        }

        public static void SetSettingsKey(DependencyObject obj, string value)
        {
            obj.SetValue(SettingsKeyProperty, value);
        }

        // Using a DependencyProperty as the backing store for SettingsKey.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SettingsKeyProperty =
            DependencyProperty.RegisterAttached("SettingsKey", typeof(string), typeof(NavigationViewExtensions), new PropertyMetadata(null));


        public static object GetSettingsParameter(DependencyObject obj)
        {
            return (object)obj.GetValue(SettingsParameterProperty);
        }

        public static void SetSettingsParameter(DependencyObject obj, object value)
        {
            obj.SetValue(SettingsParameterProperty, value);
        }

        // Using a DependencyProperty as the backing store for SettingsParameter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SettingsParameterProperty =
            DependencyProperty.RegisterAttached("SettingsParameter", typeof(object), typeof(NavigationViewExtensions), new PropertyMetadata(null));

    }
}
