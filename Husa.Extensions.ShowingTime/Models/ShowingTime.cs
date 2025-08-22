namespace Husa.Extensions.ShowingTime.Models
{
    using Husa.Extensions.ShowingTime.Interfaces;

    public class ShowingTime : IShowingTime
    {
        public AdditionalInstructions AdditionalInstructions { get; set; }
        public AppointmentRestrictions AppointmentRestrictions { get; set; }
        public AccessInformation AccessInformation { get; set; }
        public AppointmentSettings AppointmentSettings { get; set; }
    }
}
