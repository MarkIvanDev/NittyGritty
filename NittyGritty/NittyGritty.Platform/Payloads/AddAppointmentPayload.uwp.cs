using System;
using System.Text;
using NittyGritty.Platform.Appointments;
using Windows.ApplicationModel.Appointments.AppointmentsProvider;

namespace NittyGritty.Platform.Payloads
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
