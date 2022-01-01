using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp.Activation.Operations
{
    public interface IViewOperation<T> where T: IActivatedEventArgs
    {
        Task Run(T args, Frame frame);
    }
}
