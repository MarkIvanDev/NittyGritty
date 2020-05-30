using NittyGritty.Uwp.Services;
using NittyGritty.Uwp.Activation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.ApplicationModel.Appointments.AppointmentsProvider;
using Windows.Graphics.Printing.Workflow;

namespace NittyGritty.Uwp
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

        public Type Shell { get; protected set; }

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
                await ProcessActivation(args);
            }
        }

        protected sealed override async void OnActivated(IActivatedEventArgs args)
        {
            await ProcessActivation(args);
        }

        protected override async void OnBackgroundActivated(BackgroundActivatedEventArgs args)
        {
            
        }

        protected sealed override async void OnCachedFileUpdaterActivated(CachedFileUpdaterActivatedEventArgs args)
        {
            await ProcessActivation(args);
        }

        protected sealed override async void OnFileActivated(FileActivatedEventArgs args)
        {
            await ProcessActivation(args);
        }

        protected sealed override async void OnFileOpenPickerActivated(FileOpenPickerActivatedEventArgs args)
        {
            await ProcessActivation(args);
        }

        protected sealed override async void OnFileSavePickerActivated(FileSavePickerActivatedEventArgs args)
        {
            await ProcessActivation(args);
        }

        protected sealed override async void OnSearchActivated(SearchActivatedEventArgs args)
        {
            await ProcessActivation(args);
        }

        protected sealed override async void OnShareTargetActivated(ShareTargetActivatedEventArgs args)
        {
            await ProcessActivation(args);
        }

        private async Task ProcessActivation(IActivatedEventArgs args)
        {
            await Initialization();

            switch (args.Kind)
            {
                // Normal Activations
                case ActivationKind.Launch:
                    await HandleActivation((LaunchActivatedEventArgs)args);
                    break;
                case ActivationKind.CommandLineLaunch:
                    await HandleActivation((CommandLineActivatedEventArgs)args);
                    break;
                case ActivationKind.Device:
                    await HandleActivation((DeviceActivatedEventArgs)args);
                    break;
                case ActivationKind.DevicePairing:
                    await HandleActivation((DevicePairingActivatedEventArgs)args);
                    break;
                case ActivationKind.DialReceiver:
                    await HandleActivation((DialReceiverActivatedEventArgs)args);
                    break;
                case ActivationKind.File:
                    await HandleActivation((FileActivatedEventArgs)args);
                    break;
                case ActivationKind.Protocol:
                    await HandleActivation((ProtocolActivatedEventArgs)args);
                    break;
                case ActivationKind.Search:
                    await HandleActivation((SearchActivatedEventArgs)args);
                    break;
                case ActivationKind.ToastNotification:
                    await HandleActivation((ToastNotificationActivatedEventArgs)args);
                    break;
                case ActivationKind.VoiceCommand:
                    await HandleActivation((VoiceCommandActivatedEventArgs)args);
                    break;

                // Hosted Activations
                case ActivationKind.ContactPanel:
                    await HandleActivation((ContactPanelActivatedEventArgs)args);
                    break;
                case ActivationKind.FileOpenPicker:
                    await HandleActivation((FileOpenPickerActivatedEventArgs)args);
                    break;
                case ActivationKind.FileSavePicker:
                    await HandleActivation((FileSavePickerActivatedEventArgs)args);
                    break;
                case ActivationKind.ProtocolForResults:
                    await HandleActivation((ProtocolForResultsActivatedEventArgs)args);
                    break;
                case ActivationKind.ShareTarget:
                    await HandleActivation((ShareTargetActivatedEventArgs)args);
                    break;

                // Special Activations
                case ActivationKind.AppointmentsProvider when args is IAppointmentsProviderActivatedEventArgs appointmentArgs:
                    if (appointmentArgs.Verb == AppointmentsProviderLaunchActionVerbs.AddAppointment)
                    {
                        await HandleActivation((AppointmentsProviderAddAppointmentActivatedEventArgs)args);
                    }
                    else if (appointmentArgs.Verb == AppointmentsProviderLaunchActionVerbs.RemoveAppointment)
                    {
                        await HandleActivation((AppointmentsProviderRemoveAppointmentActivatedEventArgs)args);
                    }
                    else if (appointmentArgs.Verb == AppointmentsProviderLaunchActionVerbs.ReplaceAppointment)
                    {
                        await HandleActivation((AppointmentsProviderReplaceAppointmentActivatedEventArgs)args);
                    }
                    else if (appointmentArgs.Verb == AppointmentsProviderLaunchActionVerbs.ShowAppointmentDetails)
                    {
                        await HandleActivation((AppointmentsProviderShowAppointmentDetailsActivatedEventArgs)args);
                    }
                    else if (appointmentArgs.Verb == AppointmentsProviderLaunchActionVerbs.ShowTimeFrame)
                    {
                        await HandleActivation((AppointmentsProviderShowTimeFrameActivatedEventArgs)args);
                    }
                    break;
                case ActivationKind.CachedFileUpdater:
                    await HandleActivation((CachedFileUpdaterActivatedEventArgs)args);
                    break;
                case ActivationKind.LockScreen:
                    await HandleActivation((LockScreenActivatedEventArgs)args);
                    break;
                case ActivationKind.PrintWorkflowForegroundTask:
                    await HandleActivation((PrintWorkflowUIActivatedEventArgs)args);
                    break;
                case ActivationKind.WebAccountProvider:
                    await HandleActivation((WebAccountProviderActivatedEventArgs)args);
                    break;

                // Desktop only
                case ActivationKind.CameraSettings:
                case ActivationKind.ContactPicker:
                case ActivationKind.PrintTaskSettings:
                case ActivationKind.Print3DWorkflow:
                case ActivationKind.StartupTask:
                    await HandleDesktopActivation(args);
                    break;

                // Windows Store only
                case ActivationKind.Contact:
                    break;
                case ActivationKind.LockScreenCall:
                    break;
                case ActivationKind.RestrictedLaunch:
                    break;

                // Windows Phone only
                case ActivationKind.PickerReturned:
                    break;
                case ActivationKind.PickFileContinuation:
                    break;
                case ActivationKind.PickSaveFileContinuation:
                    break;
                case ActivationKind.PickFolderContinuation:
                    break;
                case ActivationKind.WalletAction:
                    break;
                case ActivationKind.WebAuthenticationBrokerContinuation:
                    break;

                // Reserved for system use
                case ActivationKind.ComponentUI:
                    break;
                case ActivationKind.FilePickerExperience:
                    break;
                case ActivationKind.GameUIProvider:
                    break;
                case ActivationKind.LockScreenComponent:
                    break;
                case ActivationKind.UserDataAccountsProvider:
                    break;

                default:
                    break;
            }

            await Startup();
        }
        #endregion

        #region Handle Normal Activation
        protected abstract Task HandleActivation(LaunchActivatedEventArgs args);

        protected virtual Task HandleActivation(CommandLineActivatedEventArgs args)
        {
            return Task.CompletedTask;
        }

        protected virtual Task HandleActivation(DeviceActivatedEventArgs args)
        {
            return Task.CompletedTask;
        }

        protected virtual Task HandleActivation(DevicePairingActivatedEventArgs args)
        {
            return Task.CompletedTask;
        }

        protected virtual Task HandleActivation(DialReceiverActivatedEventArgs args)
        {
            return Task.CompletedTask;
        }

        protected virtual Task HandleActivation(FileActivatedEventArgs args)
        {
            return Task.CompletedTask;
        }

        protected virtual Task HandleActivation(ProtocolActivatedEventArgs args)
        {
            return Task.CompletedTask;
        }

        protected virtual Task HandleActivation(SearchActivatedEventArgs args)
        {
            return Task.CompletedTask;
        }

        protected virtual Task HandleActivation(ToastNotificationActivatedEventArgs args)
        {
            return Task.CompletedTask;
        }

        public virtual Task HandleActivation(VoiceCommandActivatedEventArgs args)
        {
            return Task.CompletedTask;
        }
        #endregion

        #region Handle Hosted Activations
        public virtual Task HandleActivation(ContactPanelActivatedEventArgs args)
        {
            return Task.CompletedTask;
        }

        public virtual Task HandleActivation(FileOpenPickerActivatedEventArgs args)
        {
            return Task.CompletedTask;
        }

        public virtual Task HandleActivation(FileSavePickerActivatedEventArgs args)
        {
            return Task.CompletedTask;
        }

        public virtual Task HandleActivation(ProtocolForResultsActivatedEventArgs args)
        {
            return Task.CompletedTask;
        }

        public virtual Task HandleActivation(ShareTargetActivatedEventArgs args)
        {
            return Task.CompletedTask;
        }
        #endregion

        #region Handle Special Activations

        protected virtual Task HandleActivation(AppointmentsProviderAddAppointmentActivatedEventArgs args)
        {
            return Task.CompletedTask;
        }

        protected virtual Task HandleActivation(AppointmentsProviderRemoveAppointmentActivatedEventArgs args)
        {
            return Task.CompletedTask;
        }

        protected virtual Task HandleActivation(AppointmentsProviderReplaceAppointmentActivatedEventArgs args)
        {
            return Task.CompletedTask;
        }

        protected virtual Task HandleActivation(AppointmentsProviderShowAppointmentDetailsActivatedEventArgs args)
        {
            return Task.CompletedTask;
        }

        protected virtual Task HandleActivation(AppointmentsProviderShowTimeFrameActivatedEventArgs args)
        {
            return Task.CompletedTask;
        }

        protected virtual Task HandleActivation(CachedFileUpdaterActivatedEventArgs args)
        {
            return Task.CompletedTask;
        }

        protected virtual Task HandleActivation(LockScreenActivatedEventArgs args)
        {
            return Task.CompletedTask;
        }

        protected virtual Task HandleActivation(PrintWorkflowUIActivatedEventArgs args)
        {
            return Task.CompletedTask;
        }

        protected virtual Task HandleActivation(WebAccountProviderActivatedEventArgs args)
        {
            return Task.CompletedTask;
        }

        #endregion

        #region Handle Desktop Activations
        protected virtual Task HandleDesktopActivation(IActivatedEventArgs args)
        {
            return Task.CompletedTask;
        }
        #endregion
    }
}
