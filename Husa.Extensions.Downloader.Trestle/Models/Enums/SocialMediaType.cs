namespace Husa.Extensions.Downloader.Trestle.Models.Enums
{
    using System.Runtime.Serialization;

    public enum SocialMediaType
    {
        Blog,
        Digg,
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
        Snapchat,
        StumbleUpon,
        Tumblr,
        Twitter,
        Website,
        YouTube,
    }
}
