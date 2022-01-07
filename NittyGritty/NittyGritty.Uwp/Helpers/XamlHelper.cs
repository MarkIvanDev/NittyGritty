using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace NittyGritty.Uwp.Helpers
{
    public static class XamlHelper
    {

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

        public static bool Contains(this FrameworkElement container, FrameworkElement element)
        {
            if (element == null || container == null)
                return false;

            var elementBounds = element.TransformToVisual(container).TransformBounds(new Rect(0.0, 0.0, element.ActualWidth, element.ActualHeight));
            var containerBounds = new Rect(0.0, 0.0, container.ActualWidth, container.ActualHeight);

            return containerBounds.Contains(new Point(elementBounds.Left, elementBounds.Top)) &&
                   containerBounds.Contains(new Point(elementBounds.Left + elementBounds.Width, elementBounds.Top + elementBounds.Height));
        }

    }
}
