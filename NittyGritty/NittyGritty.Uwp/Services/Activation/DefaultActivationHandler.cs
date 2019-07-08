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
        private readonly Frame context;

        public DefaultActivationHandler(Type defaultView, Frame context)
        {
            this.defaultView = defaultView;
            this.context = context;
        }

        public override async Task HandleAsync(object args)
        {
            context.Navigate(defaultView);
            await Task.CompletedTask;
        }
    }
}
