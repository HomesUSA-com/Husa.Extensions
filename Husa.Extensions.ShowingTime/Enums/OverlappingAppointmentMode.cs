namespace Husa.Extensions.ShowingTime.Enums
{
    using System.Runtime.Serialization;

    public enum OverlappingAppointmentMode
    {
        [EnumMember(Value = "DEFAULT")]
        Default,
        [EnumMember(Value = "ALLOW_NO_WARNING")]
        AllowNoWarning,
        [EnumMember(Value = "ALLOW_WITH_WARNING")]
        AllowWithWarning,
        [EnumMember(Value = "NOT_ALLOWED")]
        NotAllowed,
    }
}
