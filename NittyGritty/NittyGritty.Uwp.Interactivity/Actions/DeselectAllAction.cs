using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xaml.Interactivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp.Interactivity.Actions
{
    public class DeselectAllAction : DependencyObject, IAction
    {
        public object Execute(object sender, object parameter)
        {
            ItemsView?.SelectedItems.Clear();
            return null;
        }

        public ListViewBase ItemsView
        {
            get { return (ListViewBase)GetValue(ItemsViewProperty); }
            set { SetValue(ItemsViewProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemsView.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsViewProperty =
            DependencyProperty.Register("ItemsView", typeof(ListViewBase), typeof(DeselectAllAction), new PropertyMetadata(null));

    }
}
