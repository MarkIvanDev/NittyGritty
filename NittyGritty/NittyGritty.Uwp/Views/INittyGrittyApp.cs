using NittyGritty.Uwp.Services.Activation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace NittyGritty.Uwp.Views
{
    public interface INittyGrittyApp
    {
        IEnumerable<IActivationHandler> GetActivationHandlers();

        DefaultActivationHandler GetDefaultHandler();

        UIElement CreateShell();

        Task Initialization();

        Task Startup();
    }
}
