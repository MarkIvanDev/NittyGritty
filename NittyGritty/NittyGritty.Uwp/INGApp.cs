using NittyGritty.Uwp.Activation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp
{
    public interface INGApp
    {
        Type Shell { get; }

        Type DefaultView { get; }

        Frame GetNavigationContext();

        Task Initialization(IActivatedEventArgs args);

        Task Startup(IActivatedEventArgs args);
    }
}
