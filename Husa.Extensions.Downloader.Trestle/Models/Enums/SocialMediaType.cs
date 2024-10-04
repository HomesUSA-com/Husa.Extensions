namespace Husa.Extensions.Downloader.Trestle.Models.Enums
{
    using System.Runtime.Serialization;

    public enum SocialMediaType
    {
        Blog,
        Digg,
        Facebook,
        FacebookMessenger,
        Googleplus,
        [EnumMember(Value = "iMessage")]
        IMessage,
        Instagram,
        Linkedin,
        Pinterest,
        Reddit,
        Slack,
        Snapchat,
        Stumbleupon,
        Tumblr,
        Twitter,
        Website,
        Youtube,
    }
}
