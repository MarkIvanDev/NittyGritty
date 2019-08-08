using NittyGritty.Platform.Appointments;
using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.UI.Platform.Payloads
{
    public partial class AddAppointmentPayload
    {
        NGAppointment PlatformAppointment => throw new NotImplementedException();

        void PlatformCanceled()
        {
            throw new NotImplementedException();
        }

        void PlatformCompleted(string appointmentId)
        {
            throw new NotImplementedException();
        }

        void PlatformFailed(string error)
        {
            throw new NotImplementedException();
        }
    }
}
