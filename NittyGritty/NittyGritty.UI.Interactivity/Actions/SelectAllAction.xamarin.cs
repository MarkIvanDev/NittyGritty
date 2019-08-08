using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NittyGritty.UI.Interactivity.Actions
{
    public class SelectAllAction : TriggerAction<View>
    {
        protected override void Invoke(View sender)
        {
            var items = ItemsView.ItemsSource;
            foreach (var item in items)
            {
                ItemsView.SelectedItems.Add(item);
            }
        }

        public SelectableItemsView ItemsView { get; set; }
    }
}
