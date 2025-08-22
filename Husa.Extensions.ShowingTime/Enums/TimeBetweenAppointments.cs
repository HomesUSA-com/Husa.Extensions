namespace Husa.Extensions.ShowingTime.Enums
{
    using System.ComponentModel;
    using System.Runtime.Serialization;

    public enum TimeBetweenAppointments
    {
        [EnumMember(Value = "0")]
        [Description("No Buffer Time")]
        None,
        [EnumMember(Value = "15")]
        [Description("15 minutes")]
        FifteenMinutes,
        [EnumMember(Value = "30")]
        [Description("30 minutes")]
        ThirtyMinutes,
        [EnumMember(Value = "45")]
        [Description("45 minutes")]
        FortyFiveMinutes,
        [EnumMember(Value = "60")]
        [Description("60 minutes")]
        SixtyMinutes,
        [EnumMember(Value = "75")]
        [Description("75 minutes")]
        SeventyFiveMinutes,
        [EnumMember(Value = "90")]
        [Description("90 minutes")]
        NinetyMinutes,
    }
}
