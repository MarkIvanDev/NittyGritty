using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NittyGritty.Platform.Appointments
{
    public class NGAppointment : ObservableObject
    {

        private string _subject;

        public string Subject
        {
            get { return _subject; }
            set { Set(ref _subject, value); }
        }

        private string _details;

        public string Details
        {
            get { return _details; }
            set { Set(ref _details, value); }
        }

        private string _location;

        public string Location
        {
            get { return _location; }
            set { Set(ref _location, value); }
        }

        private DateTimeOffset _startTime;

        public DateTimeOffset StartTime
        {
            get { return _startTime; }
            set { Set(ref _startTime, value); }
        }

        private TimeSpan _duration;

        public TimeSpan Duration
        {
            get { return _duration; }
            set { Set(ref _duration, value); }
        }

        private bool _allDay;

        public bool AllDay
        {
            get { return _allDay; }
            set { Set(ref _allDay, value); }
        }

        private TimeSpan? _reminder;

        public TimeSpan? Reminder
        {
            get { return _reminder; }
            set { Set(ref _reminder, value); }
        }

        private NGAppointmentBusyStatus _busyStatus;

        public NGAppointmentBusyStatus BusyStatus
        {
            get { return _busyStatus; }
            set { Set(ref _busyStatus, value); }
        }

        private NGAppointmentSensitivity _sensitivity;

        public NGAppointmentSensitivity Sensitivity
        {
            get { return _sensitivity; }
            set { Set(ref _sensitivity, value); }
        }

        private Uri _uri;

        public Uri Uri
        {
            get { return _uri; }
            set { Set(ref _uri, value); }
        }
        
        private NGAppointmentOrganizer _organizer;

        public NGAppointmentOrganizer Organizer
        {
            get { return _organizer; }
            set { Set(ref _organizer, value); }
        }

        private ICollection<NGAppointmentInvitee> _invitees;

        public ICollection<NGAppointmentInvitee> Invitees
        {
            get { return _invitees ?? (_invitees = new Collection<NGAppointmentInvitee>()); }
        }

        private NGAppointmentParticipantResponse _response;

        public NGAppointmentParticipantResponse Response
        {
            get { return _response; }
            set { Set(ref _response, value); }
        }

    }
}
