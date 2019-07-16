using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp.Services.Activation
{
    public class DefaultActivationHandler : ActivationHandler<object>
    {
        private readonly Type defaultView;

        public DefaultActivationHandler(Type defaultView)
        {
            this.defaultView = defaultView;
            NeedsNavigationContext = true;
        }

        public override async Task HandleAsync(object args)
        {
            NavigationContext?.Navigate(defaultView);
            await Task.CompletedTask;
        }
    }
}
