namespace Husa.Extensions.ShowingTime.Enums
{
    using System.ComponentModel;
    using System.Runtime.Serialization;

    public enum AppointmentType
    {
        [EnumMember(Value = "GO_AND_SHOW")]
        [Description("Go and Show")]
        GoAndShow,
        [EnumMember(Value = "COURTESY_CALL")]
        [Description("Courtesy Call")]
        CourtesyCall,
        [EnumMember(Value = "APPOINTMENT_REQUIRED_ANY")]
        [Description("Appointment Required, Confirm With Any")]
        AppointmentRequiredConfirmWithAny,
        [EnumMember(Value = "APPOINTMENT_REQUIRED_ALL")]
        [Description("Appointment Required, Confirm With All")]
        AppointmentRequiredConfirmWithAll,
        [EnumMember(Value = "VIEW_INSTRUCTIONS_ONLY")]
        [Description("View Instructions Only")]
        ViewInstructionsOnly,
    }
}
