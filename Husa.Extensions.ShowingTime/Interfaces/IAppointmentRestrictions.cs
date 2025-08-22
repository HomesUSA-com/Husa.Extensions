namespace Husa.Extensions.ShowingTime.Interfaces
{
    using Husa.Extensions.ShowingTime.Enums;

    public interface IAppointmentRestrictions
    {
        bool AllowRealtimeAvailabilityForBrokers { get; set; }
        bool AllowInspectionsAndWalkThroughs { get; set; }
        bool AllowAppraisals { get; set; }
        AppointmentTimeHours? RequiredTimeHours { get; set; }
        AppointmentTimeHours? SuggestedTimeHours { get; set; }
        AppointmentLength? MinShowingWindowShowings { get; set; }
        AppointmentLength? MaxShowingWindowShowings { get; set; }
        TimeBetweenAppointments? BufferTimeBetweenAppointments { get; set; }
        AdvancedNotice? AdvancedNotice { get; set; }
        OverlappingAppointmentMode? OverlappingAppointmentMode { get; set; }
    }
}
