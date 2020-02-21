using NittyGritty.Platform.Appointments;
using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Platform.Data
{
    public interface IReplaceAppointmentPayload
    {
        string AppointmentId { get; }

        NGAppointment Appointment { get; }

        DateTimeOffset? StartDate { get; }

        void Completed(string id);

        void Canceled();

        void Failed(string error);
    }
}
