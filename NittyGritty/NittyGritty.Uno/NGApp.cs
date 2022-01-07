using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uno
{
    public abstract partial class NGApp : Application
    {
        public NGApp()
        {

        }


        public virtual Type Shell { get; }

        public abstract Frame GetNavigationContext();

        public virtual Task Initialization(IActivatedEventArgs args)
        {
            return Task.CompletedTask;
        }

        public virtual Task Startup(IActivatedEventArgs args)
        {
            return Task.CompletedTask;
        }

        #region Overrides

        protected sealed override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            await ProcessActivation(args);
        }

        protected sealed override async void OnActivated(IActivatedEventArgs args)
        {
            await ProcessActivation(args);
        }

        #endregion

        #region Handle Activations
        private async Task ProcessActivation(IActivatedEventArgs args)
        {
            await Initialization(args);

            var rootFrame = Windows.UI.Xaml.Window.Current.Content as Frame;
            if (rootFrame is null)
            {
                rootFrame = new Frame();
                Windows.UI.Xaml.Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content is null)
            {
                if (!(Shell is null))
                {
                    rootFrame.Navigate(Shell);
                }
            }

            switch (args.Kind)
            {
                case ActivationKind.Launch:
                    var launchArgs = (LaunchActivatedEventArgs)args;
                    if (!launchArgs.PrelaunchActivated) await HandleActivation(launchArgs);
                    break;
                case ActivationKind.Protocol:
                    await HandleActivation((ProtocolActivatedEventArgs)args);
                    break;
                default:
                    await HandleOtherActivation(args);
                    break;
            }

            Windows.UI.Xaml.Window.Current.Activate();

            await Startup(args);
        }

        protected abstract Task HandleActivation(LaunchActivatedEventArgs args);

        protected virtual Task HandleActivation(ProtocolActivatedEventArgs args)
        {
            return Task.CompletedTask;
        }

        protected virtual Task HandleOtherActivation(IActivatedEventArgs args)
        {
            return Task.CompletedTask;
        }
        #endregion

    }
}
