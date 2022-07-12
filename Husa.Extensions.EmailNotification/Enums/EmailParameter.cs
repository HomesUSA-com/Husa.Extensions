namespace Husa.Extensions.EmailNotification.Enums
{
    using System.ComponentModel;

    public enum EmailParameter
    {
        [Description("NAME")]
        Name,
        [Description("LINK")]
        Link,
        [Description("USERNAME")]
        Username,
        [Description("PASSWORD")]
        Password,
    }
}
