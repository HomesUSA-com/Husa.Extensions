namespace Husa.Extensions.ShowingTime.Extensions
{
    using System;
    using Husa.Extensions.ShowingTime.Interfaces;

    public static class ShowingTimeExtensions
    {
        public static void UpdateShowingTime(this IShowingTime entity, IShowingTime showingTime)
        {
            ArgumentNullException.ThrowIfNull(showingTime);
            entity.AdditionalInstructions = showingTime.AdditionalInstructions;
            entity.AppointmentRestrictions = showingTime.AppointmentRestrictions;
            entity.AccessInformation = showingTime.AccessInformation;
            entity.AppointmentSettings = showingTime.AppointmentSettings;
        }
    }
}
