using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp.Services.Activation
{
    internal class DefaultActivationHandler : ActivationHandler<object>
    {
        private readonly Type defaultView;

        public DefaultActivationHandler(Type defaultView) : base(ActivationStrategy.Normal)
        {
            this.defaultView = defaultView;
        }

        protected override async Task HandleInternal(object args)
        {
            NavigationContext?.Navigate(defaultView);
            await Task.CompletedTask;
        }
    }
}
