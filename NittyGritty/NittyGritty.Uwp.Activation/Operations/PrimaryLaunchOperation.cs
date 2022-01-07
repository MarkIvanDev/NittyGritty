using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp.Activation.Operations
{
    public class PrimaryLaunchOperation : LaunchOperation
    {
        public PrimaryLaunchOperation(Type primaryView) : base(LaunchSource.Primary)
        {
            PrimaryView = primaryView ?? throw new ArgumentNullException(nameof(primaryView));
        }

        public Type PrimaryView { get; }

        public override async Task Run(LaunchActivatedEventArgs args, Frame frame)
        {
            if(frame.Content == null)
            {
                frame.Navigate(PrimaryView);
            }
            await Task.CompletedTask;
        }
    }
}
