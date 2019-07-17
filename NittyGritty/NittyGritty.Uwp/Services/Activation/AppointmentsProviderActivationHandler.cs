using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Appointments.AppointmentsProvider;

namespace NittyGritty.Uwp.Services.Activation
{
    public class AppointmentsProviderActivationHandler : ActivationHandler<IAppointmentsProviderActivatedEventArgs>
    {
        public AppointmentsProviderActivationHandler()
        {

        }

        public Func<AddAppointmentOperation, Task> AddCallback { get; private set; }

        public Func<RemoveAppointmentOperation, Task> RemoveCallback { get; private set; }

        public Func<ReplaceAppointmentOperation, Task> ReplaceCallback { get; private set; }

        public Func<AppointmentsProviderShowAppointmentDetailsActivatedEventArgs, Task> ShowDetailsCallback { get; private set; }

        public Func<AppointmentsProviderShowTimeFrameActivatedEventArgs, Task> ShowTimeFrameCallback { get; private set; }

        public sealed override async Task HandleAsync(IAppointmentsProviderActivatedEventArgs args)
        {
            switch (args)
            {
                case AppointmentsProviderAddAppointmentActivatedEventArgs add:
                    await AddCallback?.Invoke(add.AddAppointmentOperation);
                    break;

                case AppointmentsProviderRemoveAppointmentActivatedEventArgs remove:
                    await RemoveCallback?.Invoke(remove.RemoveAppointmentOperation);
                    break;

                case AppointmentsProviderReplaceAppointmentActivatedEventArgs replace:
                    await ReplaceCallback?.Invoke(replace.ReplaceAppointmentOperation);
                    break;

                case AppointmentsProviderShowAppointmentDetailsActivatedEventArgs showDetails:
                    await ShowDetailsCallback?.Invoke(showDetails);
                    break;

                case AppointmentsProviderShowTimeFrameActivatedEventArgs showTimeFrame:
                    await ShowTimeFrameCallback?.Invoke(showTimeFrame);
                    break;
            }
        }

        public sealed override bool CanHandle(IAppointmentsProviderActivatedEventArgs args)
        {
            switch (args)
            {
                case AppointmentsProviderAddAppointmentActivatedEventArgs add:
                    return AddCallback != null;

                case AppointmentsProviderRemoveAppointmentActivatedEventArgs remove:
                    return RemoveCallback != null;

                case AppointmentsProviderReplaceAppointmentActivatedEventArgs replace:
                    return ReplaceCallback != null;

                case AppointmentsProviderShowAppointmentDetailsActivatedEventArgs showDetails:
                    return ShowDetailsCallback != null;

                case AppointmentsProviderShowTimeFrameActivatedEventArgs showTimeFrame:
                    return ShowTimeFrameCallback != null;

                default:
                    return false;
            }
        }

        public AppointmentsProviderActivationHandler SetAddCallback(Func<AddAppointmentOperation, Task> addCallback)
        {
            AddCallback = addCallback;
            return this;
        }

        public AppointmentsProviderActivationHandler SetRemoveCallback(Func<RemoveAppointmentOperation, Task> removeCallback)
        {
            RemoveCallback = removeCallback;
            return this;
        }

        public AppointmentsProviderActivationHandler SetReplaceCallback(Func<ReplaceAppointmentOperation, Task> replaceCallback)
        {
            ReplaceCallback = replaceCallback;
            return this;
        }

        public AppointmentsProviderActivationHandler SetShowDetailsCallback(Func<AppointmentsProviderShowAppointmentDetailsActivatedEventArgs, Task> showDetailsCallback)
        {
            ShowDetailsCallback = showDetailsCallback;
            return this;
        }

        public AppointmentsProviderActivationHandler SetShowTimeFrameCallback(Func<AppointmentsProviderShowTimeFrameActivatedEventArgs, Task> showTimeFrameCallback)
        {
            ShowTimeFrameCallback = showTimeFrameCallback;
            return this;
        }


    }
}
