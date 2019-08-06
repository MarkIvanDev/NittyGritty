using NittyGritty.Platform.Appointments;
using NittyGritty.Platform.Payloads;
using NittyGritty.Uwp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Appointments.AppointmentsProvider;

namespace NittyGritty.UI.Platform.Payloads
{
    public class AddAppointmentPayload : IAddAppointmentPayload
    {
        private readonly AddAppointmentOperation operation;

        public AddAppointmentPayload(AddAppointmentOperation operation)
        {
            this.operation = operation;
            Appointment = operation.AppointmentInformation.ToNGAppointment();
        }

        public NGAppointment Appointment { get; }

        public void Canceled()
        {
            operation.ReportCanceled();
        }

        public void Completed(string appointmentId)
        {
            operation.ReportCompleted(appointmentId);
        }

        public void Failed(string error)
        {
            operation.ReportError(error);
        }
    }
}
