using NittyGritty.Uwp.Services.Activation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp.Views
{
    public interface INittyGrittyApp
    {
        UIElement CreateShell();

        Frame GetNavigationContext();

        IEnumerable<IActivationHandler> GetActivationHandlers();

        Type DefaultView { get; }

        Task Initialization();

        Task Startup();
    }
}
