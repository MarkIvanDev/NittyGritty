using NittyGritty.Platform.Appointments;
using NittyGritty.Platform.Payloads;
using NittyGritty.UI.Platform.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Appointments.AppointmentsProvider;

namespace NittyGritty.UI.Platform.Payloads
{
    public partial class AddAppointmentPayload
    {
        readonly AddAppointmentOperation operation;

        public AddAppointmentPayload(AddAppointmentOperation operation)
        {
            this.operation = operation;
            PlatformAppointment = operation.AppointmentInformation.ToNGAppointment();
        }

        NGAppointment PlatformAppointment { get; }

        void PlatformCanceled()
        {
            operation.ReportCanceled();
        }

        void PlatformCompleted(string appointmentId)
        {
            operation.ReportCompleted(appointmentId);
        }

        void PlatformFailed(string error)
        {
            operation.ReportError(error);
        }
    }
}
