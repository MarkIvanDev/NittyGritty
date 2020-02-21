using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Platform.Data
{
    public interface IRemoveAppointmentPayload
    {
        string AppointmentId { get; }

        DateTimeOffset? StartDate { get; }

        void Completed();

        void Canceled();

        void Failed(string error);

    }
}
