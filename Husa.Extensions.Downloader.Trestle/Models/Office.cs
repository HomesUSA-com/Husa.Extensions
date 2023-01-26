namespace Husa.Extensions.Downloader.Trestle.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using Husa.Extensions.Downloader.Trestle.Helpers.Converters;
    using Husa.Extensions.Downloader.Trestle.Models.Enums;

    public class Office
    {
        public string FranchiseAffiliation { get; set; }
        public bool? HumanModifiedYN { get; set; }
        public bool? IDXOfficeParticipationYN { get; set; }
        public string MainOfficeKey { get; set; }
        public int? MainOfficeKeyNumeric { get; set; }
        public string MainOfficeMlsId { get; set; }
        public DateTimeOffset? ModificationTimestamp { get; set; }
        public string OfficeAddress1 { get; set; }
        public string OfficeAddress2 { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<AOR?>))]
        public AOR? OfficeAOR { get; set; }
        public string OfficeAORkey { get; set; }
        public int? OfficeAORkeyNumeric { get; set; }
        public string OfficeAORMlsId { get; set; }
        public string OfficeAssociationComments { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<OfficeBranchType?>))]
        public OfficeBranchType? OfficeBranchType { get; set; }
        public string OfficeBrokerKey { get; set; }
        public int? OfficeBrokerKeyNumeric { get; set; }
        public string OfficeBrokerMlsId { get; set; }
        public string OfficeCity { get; set; }
        public string OfficeCorporateLicense { get; set; }
        public string OfficeCountyOrParish { get; set; }
        public string OfficeEmail { get; set; }
        public string OfficeFax { get; set; }
        public string OfficeKey { get; set; }
        public int? OfficeKeyNumeric { get; set; }
        public string OfficeManagerKey { get; set; }
        public int? OfficeManagerKeyNumeric { get; set; }
        public string OfficeManagerMlsId { get; set; }
        public string OfficeMlsId { get; set; }
        public string OfficeName { get; set; }
        public string OfficeNationalAssociationId { get; set; }
        public string OfficePhone { get; set; }
        public string OfficePhoneExt { get; set; }
        public string OfficePostalCode { get; set; }
        public string OfficePostalCodePlus4 { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<StateOrProvince?>))]
        public StateOrProvince? OfficeStateOrProvince { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<OfficeStatus?>))]
        public OfficeStatus? OfficeStatus { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<OfficeType?>))]
        public OfficeType? OfficeType { get; set; }
        public DateTimeOffset? OriginalEntryTimestamp { get; set; }
        public string OriginatingSystemID { get; set; }
        public string OriginatingSystemName { get; set; }
        public string OriginatingSystemOfficeKey { get; set; }
        public string OriginatingSystemSubName { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<Permission?>))]
        public Permission? Permission { get; set; }
        public string PermissionPrivate { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<SocialMediaType>))]
        public IEnumerable<SocialMediaType> SocialMediaType { get; set; }
        public string SourceSystemID { get; set; }
        public string SourceSystemName { get; set; }
        public string SourceSystemOfficeKey { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<SyndicateAgentOption>))]
        public IEnumerable<SyndicateAgentOption> SyndicateAgentOption { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<SyndicateTo?>))]
        public SyndicateTo? SyndicateTo { get; set; }
    }
}
