namespace Husa.Extensions.ShowingTime.Interfaces
{
    using Husa.Extensions.ShowingTime.Models;

    public interface IShowingTime
    {
        AdditionalInstructions AdditionalInstructions { get; set; }
        AppointmentRestrictions AppointmentRestrictions { get; set; }
        AccessInformation AccessInformation { get; set; }
        AppointmentSettings AppointmentSettings { get; set; }
    }
}
