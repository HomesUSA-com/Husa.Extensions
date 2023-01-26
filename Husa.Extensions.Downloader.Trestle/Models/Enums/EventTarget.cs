namespace Husa.Extensions.Downloader.Trestle.Models.Enums
{
    using System.Runtime.Serialization;

    public enum EventTarget
    {
        Agent,
        Broker,
        Digg,
        Email,
        Facebook,
        FacebookMessenger,
        GooglePlus,
        [EnumMember(Value = "iMessage")]
        IMessage,
        Instagram,
        LinkedIn,
        Pinterest,
        Reddit,
        Slack,
        SMS,
        Snapchat,
        StumbleUpon,
        Tumblr,
        Twitter,
        YouTube,
    }
}
