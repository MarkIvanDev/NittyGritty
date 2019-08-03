using NittyGritty.Platform.Appointments;
using NittyGritty.Platform.Payloads;
using NittyGritty.Uwp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Appointments.AppointmentsProvider;

namespace NittyGritty.Uwp.Platform.Payloads
{
    public class ReplaceAppointmentPayload : IReplaceAppointmentPayload
    {
        private readonly ReplaceAppointmentOperation operation;

        public ReplaceAppointmentPayload(ReplaceAppointmentOperation operation)
        {
            this.operation = operation;
            AppointmentId = operation.AppointmentId;
            Appointment = operation.AppointmentInformation.ToNGAppointment();
            StartDate = operation.InstanceStartDate;
        }

        public string AppointmentId { get; }

        public NGAppointment Appointment { get; }

        public DateTimeOffset? StartDate { get; }

        public void Canceled()
        {
            operation.ReportCanceled();
        }

        public void Completed(string id)
        {
            operation.ReportCompleted(id);
        }

        public void Failed(string error)
        {
            operation.ReportError(error);
        }
    }
}
