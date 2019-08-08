using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NittyGritty.UI.Interactivity.Actions
{
    public class ScrollToElementAction : TriggerAction<View>
    {
        protected override async void Invoke(View sender)
        {
            if (Element == null) throw new ArgumentNullException(nameof(Element));
            if (ScrollViewer == null) throw new ArgumentNullException(nameof(ScrollViewer));

            await ScrollViewer.ScrollToAsync(Element, ScrollToPosition.Start, true);
        }

        public View Element { get; set; }

        public ScrollView ScrollViewer { get; set; }
    }
}
