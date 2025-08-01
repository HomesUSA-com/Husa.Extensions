namespace Husa.Extensions.ShowingTime.Enums
{
    using System.ComponentModel;
    using System.Runtime.Serialization;

    public enum AppointmentTimeHours
    {
        [EnumMember(Value = "0")]
        [Description("0 hours")]
        Zero,
        [EnumMember(Value = "1")]
        [Description("1 hour")]
        One,
        [EnumMember(Value = "2")]
        [Description("2 hours")]
        Two,
        [EnumMember(Value = "3")]
        [Description("3 hours")]
        Three,
        [EnumMember(Value = "4")]
        [Description("4 hours")]
        Four,
        [EnumMember(Value = "5")]
        [Description("5 hours")]
        Five,
        [EnumMember(Value = "6")]
        [Description("6 hours")]
        Six,
        [EnumMember(Value = "7")]
        [Description("7 hours")]
        Seven,
        [EnumMember(Value = "8")]
        [Description("8 hours")]
        Eight,
        [EnumMember(Value = "9")]
        [Description("9 hours")]
        Nine,
        [EnumMember(Value = "10")]
        [Description("10 hours")]
        Ten,
        [EnumMember(Value = "11")]
        [Description("11 hours")]
        Eleven,
        [EnumMember(Value = "12")]
        [Description("12 hours")]
        Twelve,
        [EnumMember(Value = "24")]
        [Description("24 hours")]
        TwentyFour,
        [EnumMember(Value = "48")]
        [Description("48 hours")]
        FortyEight,
        [EnumMember(Value = "72")]
        [Description("72 hours")]
        SeventyTwo,
    }
}
