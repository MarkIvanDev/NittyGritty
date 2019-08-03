namespace NittyGritty.Platform.Appointments
{
    public interface INGAppointmentParticipant
    {
        string Name { get; set; }

        string Email { get; set; }
    }
}