﻿using System;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Graphics.Printing.Workflow;

namespace NittyGritty.Uwp
{
    public abstract class NGApp : Application
    {
        public NGApp()
        {
            
        }

        public static NGApp Instance { get { return Current as NGApp; } }

        #region ActivationService Initialization Requirements

        public virtual Type Shell { get; }

        public virtual Type DefaultView { get; }

        public abstract Frame GetNavigationContext();

        public virtual Task Initialization(IActivatedEventArgs args)
        {
            return Task.CompletedTask;
        }

        public virtual Task Startup(IActivatedEventArgs args)
        {
            return Task.CompletedTask;
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

        protected sealed override async void OnBackgroundActivated(BackgroundActivatedEventArgs args)
        {
            await HandleBackgroundActivation(args);
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
            await Initialization(args);

            var rootFrame = Window.Current.Content as Frame;
            if (rootFrame is null)
            {
                rootFrame = new Frame();
                Window.Current.Content = rootFrame;
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
                // Normal Activations
                case ActivationKind.Launch:
                    await HandleActivation((LaunchActivatedEventArgs)args);
                    break;
                case ActivationKind.AppointmentsProvider when args is AppointmentsProviderShowAppointmentDetailsActivatedEventArgs appointmentDetailsArgs:
                    await HandleActivation(appointmentDetailsArgs);
                    break;
                case ActivationKind.AppointmentsProvider when args is AppointmentsProviderShowTimeFrameActivatedEventArgs appointmentTimeFrameArgs:
                    await HandleActivation(appointmentTimeFrameArgs);
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
                case ActivationKind.RestrictedLaunch:
                    await HandleActivation((RestrictedLaunchActivatedEventArgs)args);
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
                case ActivationKind.AppointmentsProvider when args is AppointmentsProviderAddAppointmentActivatedEventArgs addAppointmenArgs:
                    await HandleActivation(addAppointmenArgs);
                    break;
                case ActivationKind.AppointmentsProvider when args is AppointmentsProviderRemoveAppointmentActivatedEventArgs removeAppointmentArgs:
                    await HandleActivation(removeAppointmentArgs);
                    break;
                case ActivationKind.AppointmentsProvider when args is AppointmentsProviderReplaceAppointmentActivatedEventArgs replaceAppointmentArgs:
                    await HandleActivation(replaceAppointmentArgs);
                    break;
                case ActivationKind.CachedFileUpdater:
                    await HandleActivation((CachedFileUpdaterActivatedEventArgs)args);
                    break;
                case ActivationKind.ContactPanel:
                    await HandleActivation((ContactPanelActivatedEventArgs)args);
                    break;
                case ActivationKind.FileOpenPicker:
                    await HandleActivation((FileOpenPickerActivatedEventArgs)args);
                    break;
                case ActivationKind.FileSavePicker:
                    await HandleActivation((FileSavePickerActivatedEventArgs)args);
                    break;
                case ActivationKind.LockScreen:
                    await HandleActivation((LockScreenActivatedEventArgs)args);
                    break;
                case ActivationKind.PrintWorkflowForegroundTask:
                    await HandleActivation((PrintWorkflowUIActivatedEventArgs)args);
                    break;
                case ActivationKind.ProtocolForResults:
                    await HandleActivation((ProtocolForResultsActivatedEventArgs)args);
                    break;
                case ActivationKind.ShareTarget:
                    await HandleActivation((ShareTargetActivatedEventArgs)args);
                    break;
                case ActivationKind.WebAccountProvider:
                    await HandleActivation((WebAccountProviderActivatedEventArgs)args);
                    break;

                // Desktop only
                case ActivationKind.CameraSettings:
                case ActivationKind.Contact:
                case ActivationKind.ContactPicker:
                case ActivationKind.GameUIProvider:
                case ActivationKind.LockScreenCall:
                case ActivationKind.PrintTaskSettings:
                case ActivationKind.Print3DWorkflow:
                case ActivationKind.StartupTask:
                    await HandleDesktopActivation(args);
                    break;

                // Windows Phone only
                case ActivationKind.PickerReturned:
                case ActivationKind.PickFileContinuation:
                case ActivationKind.PickSaveFileContinuation:
                case ActivationKind.PickFolderContinuation:
                case ActivationKind.WalletAction:
                case ActivationKind.WebAuthenticationBrokerContinuation:
                    await HandlePhoneActivation(args);
                    break;

                // Reserved for system use
                case ActivationKind.ComponentUI:
                case ActivationKind.FilePickerExperience:
                case ActivationKind.LockScreenComponent:
                case ActivationKind.UserDataAccountsProvider:
                    await HandleReservedActivation(args);
                    break;

                default:
                    await HandleOtherActivation(args);
                    break;
            }

            if (rootFrame.Content is null)
            {
                if (!(DefaultView is null))
                {
                    rootFrame.Navigate(DefaultView);
                }
            }

            Window.Current.Activate();

            await Startup(args);
        }
        #endregion

        #region Handle Normal Activation
        protected abstract Task HandleActivation(LaunchActivatedEventArgs args);

        protected virtual Task HandleActivation(AppointmentsProviderShowAppointmentDetailsActivatedEventArgs args)
        {
            return Task.CompletedTask;
        }

        protected virtual Task HandleActivation(AppointmentsProviderShowTimeFrameActivatedEventArgs args)
        {
            return Task.CompletedTask;
        }

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

        protected virtual Task HandleActivation(RestrictedLaunchActivatedEventArgs args)
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

        protected virtual Task HandleActivation(VoiceCommandActivatedEventArgs args)
        {
            return Task.CompletedTask;
        }
        #endregion

        #region Handle Hosted Activations

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

        protected virtual Task HandleActivation(CachedFileUpdaterActivatedEventArgs args)
        {
            return Task.CompletedTask;
        }

        protected virtual Task HandleActivation(ContactPanelActivatedEventArgs args)
        {
            return Task.CompletedTask;
        }

        protected virtual Task HandleActivation(FileOpenPickerActivatedEventArgs args)
        {
            return Task.CompletedTask;
        }

        protected virtual Task HandleActivation(FileSavePickerActivatedEventArgs args)
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

        protected virtual Task HandleActivation(ProtocolForResultsActivatedEventArgs args)
        {
            return Task.CompletedTask;
        }

        protected virtual Task HandleActivation(ShareTargetActivatedEventArgs args)
        {
            return Task.CompletedTask;
        }

        protected virtual Task HandleActivation(WebAccountProviderActivatedEventArgs args)
        {
            return Task.CompletedTask;
        }

        #endregion

        #region Handle Special Activations
        /// <summary>
        /// Handle activations available only in the Desktop
        /// Only override if you want to handle the following activation kinds:
        /// <list type="bullet">
        /// <item>
        ///     <term>CameraSettings</term>
        /// </item>
        /// <item>
        ///     <term>Contact</term>
        /// </item>
        /// <item>
        ///     <term>ContactPicker</term>
        /// </item>
        /// <item>
        ///     <term>GameUIProvider</term>
        /// </item>
        /// <item>
        ///     <term>LockScreenCall</term>
        /// </item>
        /// <item>
        ///     <term>PrintTaskSettings</term>
        /// </item>
        /// <item>
        ///     <term>Print3DWorkflow</term>
        /// </item>
        /// <item>
        ///     <term>StartupTask</term>
        /// </item>
        /// </list>
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        protected virtual Task HandleDesktopActivation(IActivatedEventArgs args)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Handle activations available only in Phones
        /// Only override if you want to handle the following activation kinds:
        /// <list type="bullet">
        /// <item>
        ///     <term>PickerReturned</term>
        /// </item>
        /// <item>
        ///     <term>PickFileContinuation</term>
        /// </item>
        /// <item>
        ///     <term>PickSaveFileContinuation</term>
        /// </item>
        /// <item>
        ///     <term>PickFolderContinuation</term>
        /// </item>
        /// <item>
        ///     <term>WalletAction</term>
        /// </item>
        /// <item>
        ///     <term>WebAuthenticationBrokerContinuation</term>
        /// </item>
        /// </list>
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        protected virtual Task HandlePhoneActivation(IActivatedEventArgs args)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Handle activations reserved for system use
        /// Only override if you want to handle the following activation kinds:
        /// <list type="bullet">
        /// <item>
        ///     <term>ComponentUI</term>
        /// </item>
        /// <item>
        ///     <term>FilePickerExperience</term>
        /// </item>
        /// <item>
        ///     <term>LockScreenComponent</term>
        /// </item>
        /// <item>
        ///     <term>UserDataAccountsProvider</term>
        /// </item>
        /// </list>
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        protected virtual Task HandleReservedActivation(IActivatedEventArgs args)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Handle activations that are not handled anywhere else
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        protected virtual Task HandleOtherActivation(IActivatedEventArgs args)
        {
            return Task.CompletedTask;
        }

        protected virtual Task HandleBackgroundActivation(BackgroundActivatedEventArgs args)
        {
            return Task.CompletedTask;
        }
        #endregion
    }
}
