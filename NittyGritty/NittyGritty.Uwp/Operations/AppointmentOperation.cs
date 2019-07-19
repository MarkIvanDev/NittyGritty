using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;

namespace NittyGritty.Uwp.Operations
{
    public class AppointmentOperation
    {
        private readonly Func<IAppointmentsProviderActivatedEventArgs, Task> callback;

        public AppointmentOperation(AppointmentAction action, Func<IAppointmentsProviderActivatedEventArgs, Task> callback)
        {
            if(action == AppointmentAction.Unknown)
            {
                throw new ArgumentException("Appointment Action cannot be Unknown");
            }

            Action = action;
            this.callback = callback;
        }

        public AppointmentAction Action { get; }

        public async Task Run(IAppointmentsProviderActivatedEventArgs args)
        {
            switch (Action)
            {
                case AppointmentAction.Add:
                    await callback?.Invoke(args as AppointmentsProviderAddAppointmentActivatedEventArgs);
                    break;
                case AppointmentAction.Remove:
                    break;
                case AppointmentAction.Replace:
                    break;
                case AppointmentAction.ShowDetails:
                    break;
                case AppointmentAction.ShowTimeFrame:
                    break;
                case AppointmentAction.Unknown:
                default:
                    break;
            }
        }
    }

    public enum AppointmentAction
    {
        Unknown = 0,
        Add = 2,
        Remove = 4,
        Replace = 8,
        ShowDetails = 16,
        ShowTimeFrame = 32
    }
}
