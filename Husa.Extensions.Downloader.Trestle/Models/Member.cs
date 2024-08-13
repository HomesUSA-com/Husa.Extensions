namespace Husa.Extensions.Downloader.Trestle.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using Husa.Extensions.Downloader.Trestle.Helpers.Converters;
    using Husa.Extensions.Downloader.Trestle.Models.Enums;

    public class Member
    {
        public bool? HumanModifiedYN { get; set; }
        public string JobTitle { get; set; }
        public DateTimeOffset? LastLoginTimestamp { get; set; }
        public string MemberAddress1 { get; set; }
        public string MemberAddress2 { get; set; }
        public string MemberAlternateId { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<AOR?>))]
        public AOR? MemberAOR { get; set; }
        public string MemberAORkey { get; set; }
        public int? MemberAORkeyNumeric { get; set; }
        public string MemberAORMlsId { get; set; }
        public string MemberAssociationComments { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<BillingPreference?>))]
        public BillingPreference? MemberBillingPreference { get; set; }
        public string MemberBio { get; set; }
        public string MemberCarrierRoute { get; set; }
        public string MemberCity { get; set; }
        public string MemberCityRegion { get; set; }
        public int? MemberCommitteeCount { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<Country?>))]
        public Country? MemberCountry { get; set; }
        public string MemberCountyOrParish { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<MemberDesignation?>))]
        public MemberDesignation? MemberDesignation { get; set; }
        public string MemberDirectPhone { get; set; }
        public string MemberEmail { get; set; }
        public string MemberFax { get; set; }
        public string MemberFirstName { get; set; }
        public string MemberFullName { get; set; }
        public string MemberHomePhone { get; set; }
        public string MemberIsAssistantTo { get; set; }
        public string MemberKey { get; set; }
        public int? MemberKeyNumeric { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<Languages?>))]
        public Languages? MemberLanguages { get; set; }
        public string MemberLastName { get; set; }
        public string MemberLoginId { get; set; }
        public bool? MemberMailOptOutYN { get; set; }
        public string MemberMiddleName { get; set; }
        public bool? MemberMlsAccessYN { get; set; }
        public string MemberMlsId { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<MemberMlsSecurityClass?>))]
        public MemberMlsSecurityClass? MemberMlsSecurityClass { get; set; }
        public string MemberMobilePhone { get; set; }
        public string MemberNamePrefix { get; set; }
        public string MemberNameSuffix { get; set; }
        public DateTime? MemberNationalAssociationEntryDate { get; set; }
        public string MemberNationalAssociationId { get; set; }
        public string MemberNickname { get; set; }
        public string MemberOfficePhone { get; set; }
        public string MemberOfficePhoneExt { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<MemberOtherPhoneType?>))]
        public MemberOtherPhoneType? MemberOtherPhoneType { get; set; }
        public string MemberPager { get; set; }
        public string MemberPassword { get; set; }
        public string MemberPhoneTTYTDD { get; set; }
        public string MemberPostalCode { get; set; }
        public string MemberPostalCodePlus4 { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<PreferredMail?>))]
        public PreferredMail? MemberPreferredMail { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<PreferredMedia?>))]
        public PreferredMedia? MemberPreferredMedia { get; set; }
        public string MemberPreferredPhone { get; set; }
        public string MemberPreferredPhoneExt { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<PreferredPublication?>))]
        public PreferredPublication? MemberPreferredPublication { get; set; }
        public string MemberPrimaryAorId { get; set; }
        public string MemberStateLicense { get; set; }
        public DateTime? MemberStateLicenseExpirationDate { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<StateOrProvince?>))]
        public StateOrProvince? MemberStateLicenseState { get; set; }
        public string MemberStateLicenseType { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<StateOrProvince?>))]
        public StateOrProvince? MemberStateOrProvince { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<MemberStatus?>))]
        public MemberStatus? MemberStatus { get; set; }
        public string MemberStreetAdditionalInfo { get; set; }
        public string MemberTollFreePhone { get; set; }
        public DateTime? MemberTransferDate { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<MemberType?>))]
        public MemberType? MemberType { get; set; }
        public string MemberVoiceMail { get; set; }
        public string MemberVoiceMailExt { get; set; }
        public string MemberVotingPrecinct { get; set; }
        public DateTimeOffset? ModificationTimestamp { get; set; }
        public string OfficeKey { get; set; }
        public int? OfficeKeyNumeric { get; set; }
        public string OfficeMlsId { get; set; }
        public string OfficeName { get; set; }
        public string OfficeNationalAssociationId { get; set; }
        public DateTimeOffset? OriginalEntryTimestamp { get; set; }
        public string OriginatingSystemID { get; set; }
        public string OriginatingSystemMemberKey { get; set; }
        public string OriginatingSystemMemberMlsSecurityClass { get; set; }
        public string OriginatingSystemName { get; set; }
        public string OriginatingSystemOfficeKey { get; set; }
        public string OriginatingSystemSubName { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<Permission?>))]
        public Permission? Permission { get; set; }
        public string PermissionPrivate { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<SocialMediaType>))]
        public IEnumerable<SocialMediaType> SocialMediaType { get; set; }
        public string SourceSystemID { get; set; }
        public string SourceSystemMemberKey { get; set; }
        public string SourceSystemName { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<SyndicateTo?>))]
        public SyndicateTo? SyndicateTo { get; set; }
        public string UniqueLicenseeIdentifier { get; set; }
    }
}
