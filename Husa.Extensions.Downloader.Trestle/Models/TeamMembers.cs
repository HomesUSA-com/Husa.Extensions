namespace Husa.Extensions.Downloader.Trestle.Models
{
    using System;
    using Husa.Extensions.Downloader.Trestle.Models.Enums;

    public class TeamMembers
    {
        public bool HumanModifiedYN { get; set; }
        public string MemberKey { get; set; }
        public int MemberKeyNumeric { get; set; }
        public string MemberLoginId { get; set; }
        public string MemberMlsId { get; set; }
        public MemberStatus? MemberStatus { get; set; }
        public DateTimeOffset ModificationTimestamp { get; set; }
        public string OfficeKey { get; set; }
        public DateTimeOffset OriginalEntryTimestamp { get; set; }
        public string OriginatingSystemID { get; set; }
        public string OriginatingSystemKey { get; set; }
        public string OriginatingSystemName { get; set; }
        public string OriginatingSystemSubName { get; set; }
        public Permission? Permission { get; set; }
        public string PermissionPrivate { get; set; }
        public string SourceSystemID { get; set; }
        public string SourceSystemKey { get; set; }
        public string SourceSystemName { get; set; }
        public TeamImpersonationLevel? TeamImpersonationLevel { get; set; }
        public string TeamKey { get; set; }
        public int TeamKeyNumeric { get; set; }
        public string TeamMemberKey { get; set; }
        public int TeamMemberKeyNumeric { get; set; }
        public string TeamMemberNationalAssociationId { get; set; }
        public string TeamMemberStateLicense { get; set; }
        public MemberType? TeamMemberType { get; set; }
    }
}
