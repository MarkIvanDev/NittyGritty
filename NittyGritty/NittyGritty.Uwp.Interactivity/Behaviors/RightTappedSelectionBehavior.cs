using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xaml.Interactivity;
using NittyGritty.Uwp.Helpers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;

namespace NittyGritty.Uwp.Interactivity.Behaviors
{
    public class RightTappedSelectionBehavior : Behavior<ListViewBase>
    {
        protected override void OnAttached()
        {
            if (AssociatedObject != null)
            {
                AssociatedObject.RightTapped += AssociatedObject_RightTapped;
            }
        }

        private void AssociatedObject_RightTapped(object sender, RightTappedRoutedEventArgs args)
        {
            var parentContainer = XamlHelper.FindParent<SelectorItem>(args.OriginalSource as DependencyObject);
            if (parentContainer != null)
            {
                var item = AssociatedObject.ItemFromContainer(parentContainer);
                switch (AssociatedObject.SelectionMode)
                {
                    case ListViewSelectionMode.Single:
                        if (!AssociatedObject.SelectedItem.Equals(item))
                        {
                            AssociatedObject.SelectedItem = item;
                        }
                        break;

                    case ListViewSelectionMode.Multiple:
                    case ListViewSelectionMode.Extended:
                        if (!AssociatedObject.SelectedItems.Contains(item))
                        {
                            AssociatedObject.SelectedItems.Clear();
                            AssociatedObject.SelectedItems.Add(item);
                        }
                        break;

                    case ListViewSelectionMode.None:
                    default:
                        break;
                }
            }
        }

        protected override void OnDetaching()
        {
            if (AssociatedObject != null)
            {
                AssociatedObject.RightTapped -= AssociatedObject_RightTapped;
            }
        }
    }
}
