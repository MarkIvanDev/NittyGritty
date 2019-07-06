using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp.Interactivity.Actions
{
    public class ScrollToElementAction : DependencyObject, IAction
    {
        public object Execute(object sender, object parameter)
        {
            if (Element == null) throw new ArgumentNullException(nameof(Element));
            if (ScrollViewer == null) throw new ArgumentNullException(nameof(ScrollViewer));

            var transform = Element.TransformToVisual((UIElement)ScrollViewer.Content);
            var position = transform.TransformPoint(new Point(0, 0));

            ScrollViewer.ChangeView(null, position.Y, null, false);
            return null;
        }

        public UIElement Element
        {
            get { return (UIElement)GetValue(ElementProperty); }
            set { SetValue(ElementProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Element.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ElementProperty =
            DependencyProperty.Register("Element", typeof(UIElement), typeof(ScrollToElementAction), new PropertyMetadata(null));


        public ScrollViewer ScrollViewer
        {
            get { return (ScrollViewer)GetValue(ScrollViewerProperty); }
            set { SetValue(ScrollViewerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ScrollViewer.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScrollViewerProperty =
            DependencyProperty.Register("ScrollViewer", typeof(ScrollViewer), typeof(ScrollToElementAction), new PropertyMetadata(null));


    }
}
