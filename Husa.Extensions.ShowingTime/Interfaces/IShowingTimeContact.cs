namespace Husa.Extensions.ShowingTime.Interfaces
{
    using Husa.Extensions.ShowingTime.Enums;

    public interface IShowingTimeContact
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string OfficePhone { get; set; }
        string MobilePhone { get; set; }
        string Email { get; set; }
        bool? ConfirmAppointmentsByOfficePhone { get; set; }
        ConfirmAppointmentCaller? ConfirmAppointmentCallerByOfficePhone { get; set; }
        bool? NotifyAppointmentChangesByOfficePhone { get; set; }
        AppointmentChangesNotificationOptions? AppointmentChangesNotificationsOptionsOfficePhone { get; set; }
        bool? ConfirmAppointmentsByMobilePhone { get; set; }
        ConfirmAppointmentCaller? ConfirmAppointmentCallerByMobilePhone { get; set; }
        bool? NotifyAppointmentChangesByMobilePhone { get; set; }
        AppointmentChangesNotificationOptions? AppointmentChangesNotificationsOptionsMobilePhone { get; set; }
        bool? ConfirmAppointmentsByText { get; set; }
        bool? NotifyAppointmentsChangesByText { get; set; }
        bool? SendOnFYIByText { get; set; }
        bool? ConfirmAppointmentsByEmail { get; set; }
        bool? NotifyAppointmentChangesByEmail { get; set; }
        bool? SendOnFYIByEmail { get; set; }
    }
}
