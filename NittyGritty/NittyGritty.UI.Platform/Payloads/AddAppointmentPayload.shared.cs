using NittyGritty.Platform.Appointments;
using NittyGritty.Platform.Payloads;
using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.UI.Platform.Payloads
{
    public partial class AddAppointmentPayload : IAddAppointmentPayload
    {
        public NGAppointment Appointment => PlatformAppointment;

        public void Canceled()
        {
            PlatformCanceled();
        }

        public void Completed(string appointmentId)
        {
            PlatformCompleted(appointmentId);
        }

        public void Failed(string error)
        {
            PlatformFailed(error);
        }
    }
}
