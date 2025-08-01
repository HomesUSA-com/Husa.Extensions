namespace Husa.Extensions.ShowingTime.Enums
{
    using System.ComponentModel;
    using System.Runtime.Serialization;

    public enum AppointmentLength
    {
        [EnumMember(Value = "1440")]
        [Description("None")]
        None,
        [EnumMember(Value = "15")]
        [Description("15 min")]
        FifteenMinutes,
        [EnumMember(Value = "30")]
        [Description("30 min")]
        ThirtyMinutes,
        [EnumMember(Value = "45")]
        [Description("45 min")]
        FortyFiveMinutes,
        [EnumMember(Value = "60")]
        [Description("1 hr")]
        OneHour,
        [EnumMember(Value = "90")]
        [Description("1 hr 30 min")]
        OneHourThirtyMinutes,
        [EnumMember(Value = "120")]
        [Description("2 hrs")]
        TwoHours,
    }
}
