namespace Husa.Extensions.ShowingTime.Models
{
    using System.Collections.Generic;
    using Husa.Extensions.Domain.ValueObjects;
    using Husa.Extensions.ShowingTime.Interfaces;

    public class ShowingTimeValueObject : ValueObject, IShowingTime
    {
        public AdditionalInstructions AdditionalInstructions { get; set; }
        public AppointmentRestrictions AppointmentRestrictions { get; set; }
        public AccessInformation AccessInformation { get; set; }
        public AppointmentSettings AppointmentSettings { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.AdditionalInstructions;
            yield return this.AppointmentRestrictions;
            yield return this.AccessInformation;
            yield return this.AppointmentSettings;
        }
    }
}
