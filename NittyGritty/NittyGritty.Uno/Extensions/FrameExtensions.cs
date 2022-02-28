using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uno.Extensions
{
    public static class FrameExtensions
    {
        public static readonly DependencyProperty KeyProperty =
            DependencyProperty.RegisterAttached("Key", typeof(string), typeof(FrameExtensions), new PropertyMetadata(null));

        public static string GetKey(Frame element)
        {
            return (string)element.GetValue(KeyProperty);
        }

        public static void SetKey(Frame element, string value)
        {
            element.SetValue(KeyProperty, value);
        }
    }
}
