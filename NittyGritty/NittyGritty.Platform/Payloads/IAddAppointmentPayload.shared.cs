using NittyGritty.Platform.Appointments;
using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Platform.Payloads
{
    public interface IAddAppointmentPayload
    {
        NGAppointment Appointment { get; }

        void Completed(string appointmentId);

        void Canceled();

        void Failed(string error);
    }
}
