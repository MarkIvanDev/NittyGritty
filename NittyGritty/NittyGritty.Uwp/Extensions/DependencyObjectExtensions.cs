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

        public static IList<T> FindChildren<T>(this DependencyObject startNode) where T : DependencyObject
        {
            var results = new List<T>();
            int count = VisualTreeHelper.GetChildrenCount(startNode);
            for (int i = 0; i < count; i++)
            {
                DependencyObject current = VisualTreeHelper.GetChild(startNode, i);
                if ((current.GetType()).Equals(typeof(T)) || (current.GetType().GetTypeInfo().IsSubclassOf(typeof(T))))
                {
                    T asType = (T)current;
                    results.Add(asType);
                }
                var children = FindChildren<T>(current);
                results.AddRange(children);
            }
            return results;
        }

        public static T FindParent<T>(this DependencyObject child) where T : DependencyObject
        {
            T parent = null;
            DependencyObject CurrentParent = VisualTreeHelper.GetParent(child);
            while (CurrentParent != null)
            {
                if (CurrentParent is T)
                {
                    parent = (T)CurrentParent;
                    break;
                }
                CurrentParent = VisualTreeHelper.GetParent(CurrentParent);

            }
            return parent;
        }
    }
}
