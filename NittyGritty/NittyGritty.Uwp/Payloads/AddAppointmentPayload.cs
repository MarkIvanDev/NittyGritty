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
    public class AddAppointmentPayload : IAddAppointmentPayload
    {
        public AddAppointmentPayload(AddAppointmentOperation operation)
        {
            Operation = operation;
            Appointment = operation.AppointmentInformation.ToNGAppointment();
        }

        public AddAppointmentOperation Operation { get; }

        public NGAppointment Appointment { get; }

        public void Canceled()
        {
            Operation.ReportCanceled();
        }

        public void Completed(string appointmentId)
        {
            Operation.ReportCompleted(appointmentId);
        }

        public void Failed(string error)
        {
            Operation.ReportError(error);
        }
    }
}
