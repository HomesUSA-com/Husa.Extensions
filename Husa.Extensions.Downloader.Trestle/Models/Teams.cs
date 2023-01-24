namespace Husa.Extensions.Downloader.Trestle.Models
{
    using System;
    using Husa.Extensions.Downloader.Trestle.Models.Enums;

    public class Teams
    {
        public bool HumanModifiedYN { get; set; }
        public DateTimeOffset ModificationTimestamp { get; set; }
        public string OfficeKey { get; set; }
        public DateTimeOffset OriginalEntryTimestamp { get; set; }
        public string OriginatingSystemID { get; set; }
        public string OriginatingSystemKey { get; set; }
        public string OriginatingSystemName { get; set; }
        public string OriginatingSystemSubName { get; set; }
        public Permission Permission { get; set; }
        public string PermissionPrivate { get; set; }
        public SocialMediaType SocialMediaType { get; set; }
        public string SourceSystemID { get; set; }
        public string SourceSystemKey { get; set; }
        public string SourceSystemName { get; set; }
        public string TeamAddress1 { get; set; }
        public string TeamAddress2 { get; set; }
        public string TeamCarrierRoute { get; set; }
        public string TeamCity { get; set; }
        public Country TeamCountry { get; set; }
        public string TeamCountyOrParish { get; set; }
        public string TeamDescription { get; set; }
        public string TeamDirectPhone { get; set; }
        public string TeamEmail { get; set; }
        public string TeamFax { get; set; }
        public string TeamKey { get; set; }
        public int TeamKeyNumeric { get; set; }
        public string TeamLeadKey { get; set; }
        public int TeamLeadKeyNumeric { get; set; }
        public string TeamLeadLoginId { get; set; }
        public string TeamLeadMlsId { get; set; }
        public string TeamLeadNationalAssociationId { get; set; }
        public string TeamLeadStateLicense { get; set; }
        public StateOrProvince TeamLeadStateLicenseState { get; set; }
        public string TeamMobilePhone { get; set; }
        public string TeamName { get; set; }
        public string TeamOfficePhone { get; set; }
        public string TeamOfficePhoneExt { get; set; }
        public string TeamPostalCode { get; set; }
        public string TeamPostalCodePlus4 { get; set; }
        public string TeamPreferredPhone { get; set; }
        public string TeamPreferredPhoneExt { get; set; }
        public StateOrProvince TeamStateOrProvince { get; set; }
        public TeamStatus TeamStatus { get; set; }
        public string TeamTollFreePhone { get; set; }
        public string TeamVoiceMail { get; set; }
        public string TeamVoiceMailExt { get; set; }
    }
}
