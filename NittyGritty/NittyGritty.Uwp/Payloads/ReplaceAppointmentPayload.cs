using NittyGritty.Platform.Appointments;
using NittyGritty.Platform.Payloads;
using NittyGritty.Uwp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Appointments.AppointmentsProvider;

namespace NittyGritty.Uwp.Payloads
{
    public class ReplaceAppointmentPayload : IReplaceAppointmentPayload
    {
        public ReplaceAppointmentPayload(ReplaceAppointmentOperation operation)
        {
            Operation = operation;
            AppointmentId = operation.AppointmentId;
            Appointment = operation.AppointmentInformation.ToNGAppointment();
            StartDate = operation.InstanceStartDate;
        }

        public ReplaceAppointmentOperation Operation { get; }

        public string AppointmentId { get; }

        public NGAppointment Appointment { get; }

        public DateTimeOffset? StartDate { get; }

        public void Canceled()
        {
            Operation.ReportCanceled();
        }

        public void Completed(string id)
        {
            Operation.ReportCompleted(id);
        }

        public void Failed(string error)
        {
            Operation.ReportError(error);
        }
    }
}
