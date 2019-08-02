using NittyGritty.Uwp.Activation.Operations;
using NittyGritty.Uwp.Activation.Operations.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp.Activation.Operations
{
    public abstract class LaunchOperation : IViewOperation<LaunchActivatedEventArgs>
    {
        public LaunchOperation(LaunchSource source)
        {
            if(source == LaunchSource.Unknown)
            {
                throw new ArgumentException("Launch Source cannot be Unknown");
            }

            Source = source;
        }

        public LaunchSource Source { get; }

        public abstract Task Run(LaunchActivatedEventArgs args, Frame frame);
    }

    public enum LaunchSource
    {
        Unknown = 0,
        Primary = 1,
        Secondary = 2,
        Jumplist = 3,
        Chaseable = 4,
    }
}
