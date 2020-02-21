using NittyGritty.Platform.Appointments;
using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Platform.Data
{
    public interface IAddAppointmentPayload
    {
        NGAppointment Appointment { get; }

        void Completed(string appointmentId);

        void Canceled();

        void Failed(string error);
    }
}
