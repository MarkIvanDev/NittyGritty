using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NittyGritty.UI.Interactivity.Actions
{
    public class DeselectAllAction : TriggerAction<View>
    {
        protected override void Invoke(View sender)
        {
            ItemsView?.SelectedItems.Clear();
        }

        public SelectableItemsView ItemsView { get; set; }
    }
}
