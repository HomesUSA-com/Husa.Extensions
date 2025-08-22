namespace Husa.Extensions.ShowingTime.Models
{
    using Husa.Extensions.ShowingTime.Enums;
    using Husa.Extensions.ShowingTime.Interfaces;

    public class AppointmentRestrictions : IAppointmentRestrictions
    {
        public bool AllowRealtimeAvailabilityForBrokers { get; set; }
        public bool AllowInspectionsAndWalkThroughs { get; set; }
        public bool AllowAppraisals { get; set; }
        public AppointmentTimeHours? RequiredTimeHours { get; set; }
        public AppointmentTimeHours? SuggestedTimeHours { get; set; }
        public AppointmentLength? MinShowingWindowShowings { get; set; }
        public AppointmentLength? MaxShowingWindowShowings { get; set; }
        public TimeBetweenAppointments? BufferTimeBetweenAppointments { get; set; }
        public AdvancedNotice? AdvancedNotice { get; set; }
        public OverlappingAppointmentMode? OverlappingAppointmentMode { get; set; }

        public AppointmentRestrictions Clone() => this.MemberwiseClone() as AppointmentRestrictions;
    }
}
