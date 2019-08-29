using NittyGritty.Uwp.Activation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp
{
    public interface INGApp
    {
        Type Shell { get; }

        Frame GetNavigationContext();

        IEnumerable<IActivationHandler> GetActivationHandlers();

        Type DefaultView { get; }

        Task Initialization();

        Task Startup();
    }
}
