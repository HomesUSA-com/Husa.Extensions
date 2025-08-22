namespace Husa.Extensions.ShowingTime.Models
{
    using Husa.Extensions.ShowingTime.Enums;
    using Husa.Extensions.ShowingTime.Interfaces;

    public class AppointmentSettings : IAppointmentSettings
    {
        public AppointmentType? AppointmentType { get; set; }
        public bool IsAgentAccompaniedShowing { get; set; }
        public bool IsFeedbackRequested { get; set; }
        public bool IsPropertyOccupied { get; set; }
        public bool AllowApptCenterTakeAppts { get; set; }
        public bool AllowShowingAgentsToRequest { get; set; }
        public FeedbackTemplate? FeedbackTemplate { get; set; }
        public StaffLanguage? RequiredStaffLanguage { get; set; }
        public AppointmentPresentationType? AppointmentPresentationType { get; set; }

        public AppointmentSettings Clone() => this.MemberwiseClone() as AppointmentSettings;
    }
}
