using NittyGritty.Uwp.Activation.Operations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Appointments.AppointmentsProvider;

namespace NittyGritty.Uwp.Activation
{
    public class AppointmentsProviderActivationHandler : ActivationHandler<IAppointmentsProviderActivatedEventArgs>
    {
        private readonly Dictionary<AppointmentAction, AppointmentOperation> operations;

        public AppointmentsProviderActivationHandler(params AppointmentOperation[] operations) : base(ActivationStrategy.Normal)
        {
            this.operations = new Dictionary<AppointmentAction, AppointmentOperation>();
            foreach (var operation in operations)
            {
                this.operations.Add(operation.Action, operation);
            }
        }

        protected sealed override async Task HandleInternal(IAppointmentsProviderActivatedEventArgs args)
        {
            switch (args)
            {
                case AppointmentsProviderAddAppointmentActivatedEventArgs add:
                    {
                        if (operations.TryGetValue(AppointmentAction.Add, out var operation))
                        {
                            await operation.Run(add, NavigationContext);
                        }
                    }
                    break;

                case AppointmentsProviderRemoveAppointmentActivatedEventArgs remove:
                    {
                        if (operations.TryGetValue(AppointmentAction.Remove, out var operation))
                        {
                            await operation.Run(remove, NavigationContext);
                        }
                    }
                    break;

                case AppointmentsProviderReplaceAppointmentActivatedEventArgs replace:
                    {
                        if (operations.TryGetValue(AppointmentAction.Replace, out var operation))
                        {
                            await operation.Run(replace, NavigationContext);
                        }
                    }
                    break;

                case AppointmentsProviderShowAppointmentDetailsActivatedEventArgs showDetails:
                    {
                        if (operations.TryGetValue(AppointmentAction.ShowDetails, out var operation))
                        {
                            await operation.Run(showDetails, NavigationContext);
                        }
                    }
                    break;

                case AppointmentsProviderShowTimeFrameActivatedEventArgs showTimeFrame:
                    {
                        if (operations.TryGetValue(AppointmentAction.ShowTimeFrame, out var operation))
                        {
                            await operation.Run(showTimeFrame, NavigationContext);
                        }
                    }
                    break;

                default:
                    // We should not reach this part. Please check if you have added all of the appointment operations this app can handle
                    break;
            }
        }

        protected sealed override bool CanHandleInternal(IAppointmentsProviderActivatedEventArgs args)
        {
            if(args.Verb == AppointmentsProviderLaunchActionVerbs.AddAppointment)
            {
                return operations.ContainsKey(AppointmentAction.Add);
            }
            else if (args.Verb == AppointmentsProviderLaunchActionVerbs.RemoveAppointment)
            {
                return operations.ContainsKey(AppointmentAction.Remove);
            }
            else if (args.Verb == AppointmentsProviderLaunchActionVerbs.ReplaceAppointment)
            {
                return operations.ContainsKey(AppointmentAction.Replace);
            }
            else if (args.Verb == AppointmentsProviderLaunchActionVerbs.ShowAppointmentDetails)
            {
                return operations.ContainsKey(AppointmentAction.ShowDetails);
            }
            else if (args.Verb == AppointmentsProviderLaunchActionVerbs.ShowTimeFrame)
            {
                return operations.ContainsKey(AppointmentAction.ShowTimeFrame);
            }
            else
            {
                return false;
            }
        }

    }
}
