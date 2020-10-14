using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace NittyGritty.Uwp.Extensions
{
    public static class DependencyObjectExtensions
    {

        public static object GetTag(DependencyObject obj)
        {
            return (object)obj.GetValue(TagProperty);
        }

        public static void SetTag(DependencyObject obj, object value)
        {
            obj.SetValue(TagProperty, value);
        }

        // Using a DependencyProperty as the backing store for Tag.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TagProperty =
            DependencyProperty.RegisterAttached("Tag", typeof(object), typeof(DependencyObjectExtensions), new PropertyMetadata(null));

    }
}
