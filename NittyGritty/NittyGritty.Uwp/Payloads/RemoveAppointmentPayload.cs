using NittyGritty.Platform.Payloads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Appointments.AppointmentsProvider;

namespace NittyGritty.Uwp.Payloads
{
    public class RemoveAppointmentPayload : IRemoveAppointmentPayload
    {
        public RemoveAppointmentPayload(RemoveAppointmentOperation operation)
        {
            Operation = operation;
            AppointmentId = operation.AppointmentId;
            StartDate = operation.InstanceStartDate;
        }

        public RemoveAppointmentOperation Operation { get; }

        public string AppointmentId { get; }

        public DateTimeOffset? StartDate { get; }

        public void Canceled()
        {
            Operation.ReportCanceled();
        }

        public void Completed()
        {
            Operation.ReportCompleted();
        }

        public void Failed(string error)
        {
            Operation.ReportError(error);
        }
    }
}
