using NittyGritty.Extensions
using NittyGritty.Platform.Appointments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Appointments;

namespace NittyGritty.Uwp.Extensions
{
    public static class NGAppointmentExtensions
    {
        public static NGAppointment ToNGAppointment(this Appointment appointment)
        {
            var ngAppointment = new NGAppointment()
            {
                Subject = appointment.Subject,
                Location = appointment.Location,
                Details = appointment.Details,
                StartTime = appointment.StartTime,
                Duration = appointment.Duration,
                AllDay = appointment.AllDay,
                Reminder = appointment.Reminder,
                BusyStatus = (NGAppointmentBusyStatus)appointment.BusyStatus,
                Sensitivity = (NGAppointmentSensitivity)appointment.Sensitivity,
                Uri = appointment.Uri,
                Organizer = appointment.Organizer.ToNGAppointmentOrganizer(),
                Response = (NGAppointmentParticipantResponse)appointment.UserResponse
            };
            ngAppointment.Invitees.AddRange(appointment.Invitees.Select(i => i.ToNGAppointmentInvitee()));
            return ngAppointment;
        }

        public static NGAppointmentOrganizer ToNGAppointmentOrganizer(this AppointmentOrganizer organizer)
        {
            var ngOrganizer = new NGAppointmentOrganizer()
            {
                Name = organizer.DisplayName,
                Email = organizer.Address
            };
            return ngOrganizer;
        }

        public static NGAppointmentInvitee ToNGAppointmentInvitee(this AppointmentInvitee invitee)
        {
            var ngInvitee = new NGAppointmentInvitee()
            {
                Role = (NGAppointmentParticipantRole)invitee.Role,
                Response = (NGAppointmentParticipantResponse)invitee.Response,
                Name = invitee.DisplayName,
                Email = invitee.Address
            };
            return ngInvitee;
        }
    }
}
