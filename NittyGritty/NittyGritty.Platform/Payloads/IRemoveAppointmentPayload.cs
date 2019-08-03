using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Platform.Payloads
{
    public interface IRemoveAppointmentPayload
    {
        string AppointmentId { get; }

        DateTimeOffset? InstanceStartDate { get; }

        void Completed();

        void Canceled();

        void Failed(string error);

    }
}
