namespace Husa.Extensions.ShowingTime.Interfaces
{
    using Husa.Extensions.ShowingTime.Enums;

    public interface IAppointmentSettings
    {
        AppointmentType? AppointmentType { get; set; }
        bool IsAgentAccompaniedShowing { get; set; }
        bool IsFeedbackRequested { get; set; }
        bool IsPropertyOccupied { get; set; }
        bool AllowApptCenterTakeAppts { get; set; }
        bool AllowShowingAgentsToRequest { get; set; }
        FeedbackTemplate? FeedbackTemplate { get; set; }
        StaffLanguage? RequiredStaffLanguage { get; set; }
        AppointmentPresentationType? AppointmentPresentationType { get; set; }
    }
}
