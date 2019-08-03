namespace NittyGritty.Platform.Appointments
{
    public class NGAppointmentInvitee : ObservableObject, INGAppointmentParticipant
    {

        private NGAppointmentParticipantRole _role;

        public NGAppointmentParticipantRole Role
        {
            get { return _role; }
            set { Set(ref _role, value); }
        }

        private NGAppointmentParticipantResponse _response;

        public NGAppointmentParticipantResponse Response
        {
            get { return _response; }
            set { Set(ref _response, value); }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { Set(ref _name, value); }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set { Set(ref _email, value); }
        }

    }
}