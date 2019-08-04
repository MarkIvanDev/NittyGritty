using NittyGritty.Extensions;
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
                BusyStatus = (NGBusyStatus)appointment.BusyStatus,
                Sensitivity = (NGSensitivity)appointment.Sensitivity,
                Uri = appointment.Uri,
                Organizer = appointment.Organizer.ToNGOrganizer(),
                Response = (NGParticipantResponse)appointment.UserResponse
            };
            ngAppointment.Invitees.AddRange(appointment.Invitees.Select(i => i.ToNGInvitee()));
            return ngAppointment;
        }

        public static NGOrganizer ToNGOrganizer(this AppointmentOrganizer organizer)
        {
            var ngOrganizer = new NGOrganizer()
            {
                Name = organizer.DisplayName,
                Email = organizer.Address
            };
            return ngOrganizer;
        }

        public static NGInvitee ToNGInvitee(this AppointmentInvitee invitee)
        {
            var ngInvitee = new NGInvitee()
            {
                Role = (NGParticipantRole)invitee.Role,
                Response = (NGParticipantResponse)invitee.Response,
                Name = invitee.DisplayName,
                Email = invitee.Address
            };
            return ngInvitee;
        }
    }
}
