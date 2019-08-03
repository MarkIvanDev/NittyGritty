using NittyGritty.Platform.Appointments;
using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Platform.Payloads
{
    public interface IReplaceAppointmentPayload
    {
        string AppointmentId { get; }

        NGAppointment Appointment { get; }

        DateTimeOffset? InstanceStartDate { get; }

        void Completed(string id);

        void Canceled();

        void Failed(string error);
    }
}
