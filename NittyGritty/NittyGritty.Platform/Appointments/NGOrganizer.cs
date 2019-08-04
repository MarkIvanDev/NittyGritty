namespace NittyGritty.Platform.Appointments
{
    public class NGOrganizer : ObservableObject, INGParticipant
    {

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