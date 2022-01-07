using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp.Activation.Operations
{
    public abstract class AppointmentOperation : IViewOperation<IAppointmentsProviderActivatedEventArgs>
    {
        public AppointmentOperation(AppointmentAction action, Type view)
        {
            if(action == AppointmentAction.Unknown)
            {
                throw new ArgumentException("Action cannot be unknown");
            }

            Action = action;
            View = view ?? throw new ArgumentNullException(nameof(view));
        }

        public AppointmentAction Action { get; }

        public Type View { get; }

        public abstract Task Run(IAppointmentsProviderActivatedEventArgs args, Frame frame);
    }

    public enum AppointmentAction
    {
        Unknown = 0,
        Add = 1,
        Remove = 2,
        Replace = 3,
        ShowDetails = 4,
        ShowTimeFrame = 5
    }
}
