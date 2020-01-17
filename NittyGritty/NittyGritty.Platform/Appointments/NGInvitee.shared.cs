namespace NittyGritty.Platform.Appointments
{
    public class NGInvitee : ObservableObject, INGParticipant
    {

        private NGParticipantRole _role;

        public NGParticipantRole Role
        {
            get { return _role; }
            set { Set(ref _role, value); }
        }

        private NGParticipantResponse _response;

        public NGParticipantResponse Response
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