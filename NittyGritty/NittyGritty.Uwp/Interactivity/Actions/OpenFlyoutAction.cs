using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;

namespace NittyGritty.Uwp.Interactivity.Actions
{
    public class OpenFlyoutAction : DependencyObject, IAction
    {
        public object Execute(object sender, object parameter)
        {
            var element = TargetObject ?? (FrameworkElement)sender;
            var attachedFlyout = FlyoutBase.GetAttachedFlyout(element);
            switch (attachedFlyout)
            {
                case MenuFlyout menu:
                    if (parameter is RightTappedRoutedEventArgs e)
                    {
                        menu.ShowAt(element, e.GetPosition(element));
                    }
                    else
                    {
                        menu.ShowAt(element);
                    }
                    break;
                default:
                    FlyoutBase.ShowAttachedFlyout(element);
                    break;
            }
            return null;
        }

        public FrameworkElement TargetObject
        {
            get { return (FrameworkElement)GetValue(TargetObjectProperty); }
            set { SetValue(TargetObjectProperty, value); }
        }

        public static readonly DependencyProperty TargetObjectProperty =
            DependencyProperty.Register(nameof(TargetObject), typeof(FrameworkElement), typeof(OpenFlyoutAction), new PropertyMetadata(null));

    }
}
