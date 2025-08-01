namespace Husa.Extensions.ShowingTime.Extensions
{
    using System;
    using Husa.Extensions.ShowingTime.Interfaces;
    using Husa.Extensions.ShowingTime.Models;

    public static class ShowingTimeExtensions
    {
        public static void UpdateShowingTime(this IShowingTime entity, ShowingTimeValueObject showingTime)
        {
            ArgumentNullException.ThrowIfNull(showingTime);
            entity.AdditionalInstructions = showingTime.AdditionalInstructions;
            entity.AppointmentRestrictions = showingTime.AppointmentRestrictions;
            entity.AccessInformation = showingTime.AccessInformation;
            entity.AppointmentSettings = showingTime.AppointmentSettings;
        }
    }
}
