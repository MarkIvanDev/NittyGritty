using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xaml.Interactivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Markup;

namespace NittyGritty.Uwp.Interactivity.Actions
{
    public class UnloadAction : DependencyObject, IAction
    {
        public object Execute(object sender, object parameter)
        {
            try
            {
                if (sender is FrameworkElement fe && ObjectName != null)
                {
                    var element = fe.FindName(ObjectName) as DependencyObject;
                    if (element != null)
                    {
                        XamlMarkupHelper.UnloadObject(element);
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public string ObjectName
        {
            get { return (string)GetValue(ObjectNameProperty); }
            set { SetValue(ObjectNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Object.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ObjectNameProperty =
            DependencyProperty.Register("Object", typeof(string), typeof(UnloadAction), new PropertyMetadata(null));



    }
}
