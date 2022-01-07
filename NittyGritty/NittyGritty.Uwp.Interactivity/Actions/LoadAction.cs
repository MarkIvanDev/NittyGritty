using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xaml.Interactivity;
using Windows.UI.Xaml;

namespace NittyGritty.Uwp.Interactivity.Actions
{
    public class LoadAction : DependencyObject, IAction
    {
        public object Execute(object sender, object parameter)
        {
            if (sender is FrameworkElement fe && ObjectName != null)
            {
                var element = fe.FindName(ObjectName);
                return true;
            }
            return false;
        }


        public string ObjectName
        {
            get { return (string)GetValue(ObjectNameProperty); }
            set { SetValue(ObjectNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ObjectName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ObjectNameProperty =
            DependencyProperty.Register("ObjectName", typeof(string), typeof(LoadAction), new PropertyMetadata(null));


    }
}
