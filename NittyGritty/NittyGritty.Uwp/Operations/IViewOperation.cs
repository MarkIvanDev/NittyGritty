using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp.Operations
{
    public interface IViewOperation<T>
    {
        Task Run(T args, Frame frame);
    }
}
