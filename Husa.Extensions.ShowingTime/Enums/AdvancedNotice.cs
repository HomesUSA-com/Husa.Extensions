namespace Husa.Extensions.ShowingTime.Enums
{
    using System.ComponentModel;
    using System.Runtime.Serialization;

    public enum AdvancedNotice
    {
        [EnumMember(Value = "False")]
        [Description("No same day appts.")]
        NoSameDayAppts,
        [EnumMember(Value = "True")]
        [Description("Lead Time")]
        LeadTime,
    }
}
