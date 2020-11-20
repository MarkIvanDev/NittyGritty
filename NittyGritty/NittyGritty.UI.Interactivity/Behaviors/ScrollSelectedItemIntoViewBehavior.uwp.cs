using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xaml.Interactivity;
using NittyGritty.UI.Helpers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.UI.Interactivity.Behaviors
{
    public class ScrollSelectedItemIntoViewBehavior : Behavior<ListViewBase>
    {
        private ScrollViewer internalScrollViewer = null;

        protected override void OnAttached()
        {
            if (AssociatedObject != null)
            {
                AssociatedObject.Loaded += AssociatedObject_Loaded;
                AssociatedObject.SelectionChanged += AssociatedObject_SelectionChanged;
            }
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            internalScrollViewer = XamlHelper.FindChildren<ScrollViewer>(AssociatedObject).FirstOrDefault();
            ScrollIntoView();
        }

        private void AssociatedObject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ScrollIntoView();
        }

        private void ScrollIntoView()
        {
            if (AssociatedObject != null)
            {
                if (internalScrollViewer is null)
                {
                    internalScrollViewer = XamlHelper.FindChildren<ScrollViewer>(AssociatedObject).FirstOrDefault();
                }
                object item = null;
                switch (AssociatedObject.SelectionMode)
                {
                    case ListViewSelectionMode.Single:
                        item = AssociatedObject.SelectedItem;
                        break;

                    case ListViewSelectionMode.Multiple:
                    case ListViewSelectionMode.Extended:
                        item = AssociatedObject.SelectedItems.FirstOrDefault();
                        break;

                    case ListViewSelectionMode.None:
                    default:
                        break;
                }

                if (item != null)
                {
                    var itemContainer = AssociatedObject.ContainerFromItem(item) as FrameworkElement;
                    if (itemContainer == null || !XamlHelper.Contains(internalScrollViewer, itemContainer))
                    {
                        AssociatedObject.ScrollIntoView(item, ScrollIntoViewAlignment.Leading);
                    }
                }
            }
        }

        protected override void OnDetaching()
        {
            if (AssociatedObject != null)
            {
                AssociatedObject.Loaded -= AssociatedObject_Loaded;
                AssociatedObject.SelectionChanged -= AssociatedObject_SelectionChanged;
                internalScrollViewer = null;
            }
        }

    }
}
