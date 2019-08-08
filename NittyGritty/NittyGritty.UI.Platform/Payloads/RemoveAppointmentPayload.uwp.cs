using NittyGritty.Platform.Payloads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Appointments.AppointmentsProvider;

namespace NittyGritty.UI.Platform.Payloads
{
    public class RemoveAppointmentPayload : IRemoveAppointmentPayload
    {
        private readonly RemoveAppointmentOperation operation;

        public RemoveAppointmentPayload(RemoveAppointmentOperation operation)
        {
            this.operation = operation;
            AppointmentId = operation.AppointmentId;
            StartDate = operation.InstanceStartDate;
        }

        public string AppointmentId { get; }

        public DateTimeOffset? StartDate { get; }

        public void Canceled()
        {
            operation.ReportCanceled();
        }

        public void Completed()
        {
            operation.ReportCompleted();
        }

        public void Failed(string error)
        {
            operation.ReportError(error);
        }
    }
}
