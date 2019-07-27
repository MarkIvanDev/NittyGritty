using NittyGritty.Uwp.Services;
using NittyGritty.Uwp.Services.Activation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp.Views
{
    public abstract class NGApp : Application, INGApp
    {
        public NGApp()
        {
            Current = this;

            Suspending += App_Suspending;

            _activationService = new Lazy<ActivationService>(CreateActivationService);
        }

        public static new NGApp Current { get; private set; }

        private async void App_Suspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            await SuspensionManager.SaveAsync();
            deferral.Complete();
        }

        #region ActivationService Initialization Requirements

        private readonly Lazy<ActivationService> _activationService;

        protected ActivationService ActivationService
        {
            get { return _activationService.Value; }
        }

        private ActivationService CreateActivationService()
        {
            return new ActivationService(this);
        }

        public virtual async Task Initialization()
        {
            await Task.CompletedTask;
        }

        public virtual UIElement CreateShell()
        {
            return new Frame();
        }

        public abstract Frame GetNavigationContext();

        public virtual IEnumerable<IActivationHandler> GetActivationHandlers()
        {
            return Enumerable.Empty<IActivationHandler>();
        }

        public Type DefaultView { get; protected set; }

        public virtual async Task Startup()
        {
            await Task.CompletedTask;
        }

        #endregion

        #region Activation Overrides
        protected sealed override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            if (!args.PrelaunchActivated)
            {
                await CallActivation(args);
            }
        }

        protected sealed override async void OnActivated(IActivatedEventArgs args)
        {
            await CallActivation(args);
        }

        protected override async void OnBackgroundActivated(BackgroundActivatedEventArgs args)
        {
            await CallActivation(args);
        }

        protected sealed override async void OnCachedFileUpdaterActivated(CachedFileUpdaterActivatedEventArgs args)
        {
            await CallActivation(args);
        }

        protected sealed override async void OnFileActivated(FileActivatedEventArgs args)
        {
            await CallActivation(args);
        }

        protected sealed override async void OnFileOpenPickerActivated(FileOpenPickerActivatedEventArgs args)
        {
            await CallActivation(args);
        }

        protected sealed override async void OnFileSavePickerActivated(FileSavePickerActivatedEventArgs args)
        {
            await CallActivation(args);
        }

        protected sealed override async void OnSearchActivated(SearchActivatedEventArgs args)
        {
            await CallActivation(args);
        }

        protected sealed override async void OnShareTargetActivated(ShareTargetActivatedEventArgs args)
        {
            await CallActivation(args);
        }

        private async Task CallActivation(object args)
        {
            await ActivationService.ActivateAsync(args);
        }
        #endregion

    }
}
