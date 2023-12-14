namespace Husa.Extensions.Downloader.Trestle.Models.Enums
{
    using System.ComponentModel;
    using System.Runtime.Serialization;

    public enum SystemOrigin
    {
        [EnumMember(Value = "CTEXAS")]
        [Description("CTX")]
        CTX,
        [EnumMember(Value = "ACTRIS")]
        [Description("ABOR")]
        ABOR,
        [EnumMember(Value = "HAR")]
        [Description("HOUSTON")]
        HAR,
    }
}
