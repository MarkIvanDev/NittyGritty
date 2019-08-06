using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.UI.Interactivity.Actions
{
    public class FocusAction : DependencyObject, IAction
    {
        public object Execute(object sender, object parameter)
        {
            bool result = false;
            if (sender is Control control)
            {
                result = control.Focus(FocusState.Programmatic);
            }
            return result;
        }
    }
}
