namespace Husa.Extensions.ShowingTime.Enums
{
    using System.ComponentModel;
    using System.Runtime.Serialization;

    public enum StaffLanguage
    {
        [EnumMember(Value = "ENGLISH")]
        [Description("English")]
        English,
        [EnumMember(Value = "SPANISH")]
        [Description("Spanish")]
        Spanish,
        [EnumMember(Value = "CANTONESE")]
        [Description("Cantonese")]
        Cantonese,
        [EnumMember(Value = "MANDARIN")]
        [Description("Mandarin")]
        Mandarin,
    }
}
