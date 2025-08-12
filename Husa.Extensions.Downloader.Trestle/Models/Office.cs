namespace Husa.Extensions.Downloader.Trestle.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using Husa.Extensions.Downloader.Trestle.Helpers.Converters;
    using Husa.Extensions.Downloader.Trestle.Models.Enums;

    public class Office
    {
        public string BillingOfficeKey { get; set; }

        public string FranchiseAffiliation { get; set; }

        public string FranchiseNationalAssociationId { get; set; }

        public bool? HumanModifiedYN { get; set; }

        public bool? IDXOfficeParticipationYN { get; set; }

        public string MainOfficeKey { get; set; }

        public int? MainOfficeKeyNumeric { get; set; }

        public string MainOfficeMlsId { get; set; }

        public DateTimeOffset? ModificationTimestamp { get; set; }

        public int? NumberOfBranches { get; set; }

        public int? NumberOfNonMemberSalespersons { get; set; }

        public string OfficeAddress1 { get; set; }

        public string OfficeAddress2 { get; set; }

        public string OfficeAlternateId { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<AOR?>))]
        public AOR? OfficeAOR { get; set; }

        public string OfficeAORkey { get; set; }

        public int? OfficeAORkeyNumeric { get; set; }

        public string OfficeAORMlsId { get; set; }

        public string OfficeAssociationComments { get; set; }

        public string OfficeBio { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<OfficeBranchType?>))]
        public OfficeBranchType? OfficeBranchType { get; set; }

        public string OfficeBrokerKey { get; set; }

        public int? OfficeBrokerKeyNumeric { get; set; }

        public string OfficeBrokerMlsId { get; set; }

        public string OfficeBrokerNationalAssociationId { get; set; }

        public string OfficeCity { get; set; }

        public string OfficeCityRegion { get; set; }

        public string OfficeCorporateLicense { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<Country?>))]
        public Country? OfficeCountry { get; set; }

        public string OfficeCountyOrParish { get; set; }

        public string OfficeEmail { get; set; }

        public string OfficeFax { get; set; }

        public string OfficeKey { get; set; }

        public int? OfficeKeyNumeric { get; set; }

        public string OfficeMailAddress1 { get; set; }

        public string OfficeMailAddress2 { get; set; }

        public string OfficeMailCareOf { get; set; }

        public string OfficeMailCity { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<Country?>))]
        public Country? OfficeMailCountry { get; set; }

        public string OfficeMailCountyOrParish { get; set; }

        public string OfficeMailPostalCode { get; set; }

        public string OfficeMailPostalCodePlus4 { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<StateOrProvince?>))]
        public StateOrProvince? OfficeMailStateOrProvince { get; set; }

        public string OfficeManagerKey { get; set; }

        public int? OfficeManagerKeyNumeric { get; set; }

        public string OfficeManagerMlsId { get; set; }

        public string OfficeMlsId { get; set; }

        public string OfficeName { get; set; }

        public string OfficeNationalAssociationId { get; set; }

        public DateTime? OfficeNationalAssociationIdInsertDate { get; set; }

        public string OfficePhone { get; set; }

        public string OfficePhoneExt { get; set; }

        public string OfficePostalCode { get; set; }

        public string OfficePostalCodePlus4 { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<PreferredMedia?>))]
        public PreferredMedia? OfficePreferredMedia { get; set; }

        public string OfficePrimaryAorId { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<StateOrProvince?>))]
        public StateOrProvince? OfficePrimaryStateOrProvince { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<StateOrProvince?>))]
        public StateOrProvince? OfficeStateOrProvince { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<OfficeStatus?>))]
        public OfficeStatus? OfficeStatus { get; set; }

        public string OfficeStreetAdditionalInfo { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<OfficeType?>))]
        public OfficeType? OfficeType { get; set; }

        public DateTimeOffset? OriginalEntryTimestamp { get; set; }

        public string OriginatingSystemID { get; set; }

        public string OriginatingSystemMainOfficeKey { get; set; }

        public string OriginatingSystemName { get; set; }

        public string OriginatingSystemOfficeBrokerKey { get; set; }

        public string OriginatingSystemOfficeKey { get; set; }

        public string OriginatingSystemOfficeManagerKey { get; set; }

        public string OriginatingSystemSubName { get; set; }

        public string OtherPhone { get; set; }

        [JsonConverter(typeof(StringListEnumConverter<ListingPermission>))]
        public IEnumerable<ListingPermission> Permission { get; set; }

        public int? RecordSignature { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<SocialMediaType?>))]
        public SocialMediaType? SocialMediaType { get; set; }

        public string SourceSystemID { get; set; }

        public string SourceSystemName { get; set; }

        public string SourceSystemOfficeKey { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<SyndicateAgentOption?>))]
        public SyndicateAgentOption? SyndicateAgentOption { get; set; }

        [JsonConverter(typeof(StringListEnumConverter<SyndicateTo>))]
        public IEnumerable<SyndicateTo> SyndicateTo { get; set; }

        public bool? VirtualOfficeWebsiteYN { get; set; }

        public IEnumerable<Media> Media { get; set; }

        public IEnumerable<Property> BuyerOfficeProperties { get; set; }

        public IEnumerable<Property> ListOfficeProperties { get; set; }

        public IEnumerable<Property> CoListOfficeProperties { get; set; }

        public IEnumerable<Property> CoBuyerOfficeProperties { get; set; }
    }
}
