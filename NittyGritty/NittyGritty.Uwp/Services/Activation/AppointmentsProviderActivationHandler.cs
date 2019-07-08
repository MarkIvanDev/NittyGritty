using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;

namespace NittyGritty.Uwp.Services.Activation
{
    public class AppointmentsProviderActivationHandler : ActivationHandler<IAppointmentsProviderActivatedEventArgs>
    {
        public Func<AppointmentsProviderAddAppointmentActivatedEventArgs, Task> AddAppointment { get; set; }

        public Func<AppointmentsProviderRemoveAppointmentActivatedEventArgs, Task> RemoveAppointment { get; set; }

        public Func<AppointmentsProviderReplaceAppointmentActivatedEventArgs, Task> ReplaceAppointment { get; set; }

        public Func<AppointmentsProviderShowAppointmentDetailsActivatedEventArgs, Task> ShowAppointmentDetails { get; set; }

        public Func<AppointmentsProviderShowTimeFrameActivatedEventArgs, Task> ShowTimeFrame { get; set; }

        public sealed override async Task HandleAsync(IAppointmentsProviderActivatedEventArgs args)
        {
            switch (args)
            {
                case AppointmentsProviderAddAppointmentActivatedEventArgs add:
                    await AddAppointment?.Invoke(add);
                    break;
                case AppointmentsProviderRemoveAppointmentActivatedEventArgs remove:
                    await RemoveAppointment?.Invoke(remove);
                    break;
                case AppointmentsProviderReplaceAppointmentActivatedEventArgs replace:
                    await ReplaceAppointment?.Invoke(replace);
                    break;
                case AppointmentsProviderShowAppointmentDetailsActivatedEventArgs showDetails:
                    await ShowAppointmentDetails?.Invoke(showDetails);
                    break;
                case AppointmentsProviderShowTimeFrameActivatedEventArgs showTimeFrame:
                    await ShowTimeFrame?.Invoke(showTimeFrame);
                    break;
            }
        }

        public sealed override bool CanHandle(IAppointmentsProviderActivatedEventArgs args)
        {
            switch (args)
            {
                case AppointmentsProviderAddAppointmentActivatedEventArgs add:
                    if(AddAppointment != null)
                    {
                        return true;
                    }
                    break;
                case AppointmentsProviderRemoveAppointmentActivatedEventArgs remove:
                    if(RemoveAppointment != null)
                    {
                        return true;
                    }
                    break;
                case AppointmentsProviderReplaceAppointmentActivatedEventArgs replace:
                    if(ReplaceAppointment != null)
                    {
                        return true;
                    }
                    break;
                case AppointmentsProviderShowAppointmentDetailsActivatedEventArgs showDetails:
                    if(ShowAppointmentDetails != null)
                    {
                        return true;
                    }
                    break;
                case AppointmentsProviderShowTimeFrameActivatedEventArgs showTimeFrame:
                    if(ShowTimeFrame != null)
                    {
                        return true;
                    }
                    break;
            }
            return false;
        }
    }
}
