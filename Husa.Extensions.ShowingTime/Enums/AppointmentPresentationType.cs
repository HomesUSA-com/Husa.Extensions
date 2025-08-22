namespace Husa.Extensions.ShowingTime.Enums
{
    using System.ComponentModel;
    using System.Runtime.Serialization;

    public enum AppointmentPresentationType
    {
        [EnumMember(Value = "3")]
        [Description("In-Person and Virtual Appts")]
        InPersonAndVirtualAppts,
        [EnumMember(Value = "1")]
        [Description("Virtual Appts Only")]
        VirtualApptsOnly,
        [EnumMember(Value = "2")]
        [Description("In-Person Appts Only")]
        InPersonApptsOnly,
    }
}
