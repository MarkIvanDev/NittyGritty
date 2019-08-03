using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.Uwp.Activation.Operations
{
    public abstract class AppointmentOperation
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
