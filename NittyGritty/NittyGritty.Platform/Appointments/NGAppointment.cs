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

        private NGBusyStatus _busyStatus;

        public NGBusyStatus BusyStatus
        {
            get { return _busyStatus; }
            set { Set(ref _busyStatus, value); }
        }

        private NGSensitivity _sensitivity;

        public NGSensitivity Sensitivity
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
        
        private NGOrganizer _organizer;

        public NGOrganizer Organizer
        {
            get { return _organizer; }
            set { Set(ref _organizer, value); }
        }

        private ICollection<NGInvitee> _invitees;

        public ICollection<NGInvitee> Invitees
        {
            get { return _invitees ?? (_invitees = new Collection<NGInvitee>()); }
        }

        private NGParticipantResponse _response;

        public NGParticipantResponse Response
        {
            get { return _response; }
            set { Set(ref _response, value); }
        }

    }
}
