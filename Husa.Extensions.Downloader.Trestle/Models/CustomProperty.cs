namespace Husa.Extensions.Downloader.Trestle.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using Husa.Extensions.Downloader.Trestle.Helpers.Converters;
    using Husa.Extensions.Downloader.Trestle.Models.Enums;

    public class CustomProperty
    {
        public string AboveGradeBedrooms { get; set; }

        public string AboveGradeFinishedAreaRange { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<AreaSource?>))]
        public AreaSource? AboveGradeFinishedAreaRangeSource { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<AreaUnits?>))]
        public AreaUnits? AboveGradeFinishedAreaRangeUnits { get; set; }

        public string AboveGradeUnfinishedAreaRange { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<AreaSource?>))]
        public AreaSource? AboveGradeUnfinishedAreaRangeSource { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<AreaUnits?>))]
        public AreaUnits? AboveGradeUnfinishedAreaRangeUnits { get; set; }

        public decimal? AdditionalFee { get; set; }

        public string AdditionalFeeDescription { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<FeeFrequency?>))]
        public FeeFrequency? AdditionalFeeFrequency { get; set; }

        public bool? AdditionalFeeYN { get; set; }

        public string AdditionalInfo1 { get; set; }

        public string AdditionalInfo2 { get; set; }

        public string AdditionalInfo3 { get; set; }

        public decimal? ApplicationFee { get; set; }

        public decimal? AssociationFeeTotal { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<FeeFrequency?>))]
        public FeeFrequency? AssociationFeeTotalFrequency { get; set; }

        [JsonConverter(typeof(StringListEnumConverter<Attic>))]
        public IEnumerable<Attic> Attic { get; set; }

        [JsonConverter(typeof(StringListEnumConverter<AvailabilityType>))]
        public IEnumerable<AvailabilityType> AvailabilityType { get; set; }

        public string BelowGradeBedrooms { get; set; }

        public string BelowGradeFinishedAreaRange { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<AreaSource?>))]
        public AreaSource? BelowGradeFinishedAreaRangeSource { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<AreaUnits?>))]
        public AreaUnits? BelowGradeFinishedAreaRangeUnits { get; set; }

        public string BelowGradeUnfinishedAreaRange { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<AreaSource?>))]
        public AreaSource? BelowGradeUnfinishedAreaRangeSource { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<AreaUnits?>))]
        public AreaUnits? BelowGradeUnfinishedAreaRangeUnits { get; set; }

        public string BoatDockAccommodates { get; set; }

        public decimal? BoatDockHeight { get; set; }

        public string BoatDockSlipDescription { get; set; }

        [JsonConverter(typeof(StringListEnumConverter<BoatDockSlipFeatures>))]
        public IEnumerable<BoatDockSlipFeatures> BoatDockSlipFeatures { get; set; }

        public bool? BoatDockYN { get; set; }

        public bool? BoatSlipYN { get; set; }

        public decimal? BonusAmount { get; set; }

        public string BuildingAreaTotalRange { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<AreaSource?>))]
        public AreaSource? BuildingAreaTotalRangeSource { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<AreaUnits?>))]
        public AreaUnits? BuildingAreaTotalRangeUnits { get; set; }

        public string BuildingSizeDimensions { get; set; }

        public bool? CommunityDevelopmentDistrictYN { get; set; }

        public string ComplexName { get; set; }

        public string ConsumerRemarks { get; set; }

        public string CustomFields { get; set; }

        public string DevelopmentName { get; set; }

        public string FractionalShare { get; set; }

        public string GarageArea { get; set; }

        public string GarageAreaUnits { get; set; }

        public string GarageDimensions { get; set; }

        public decimal? GuestHouseAreaTotal { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<AreaSource?>))]
        public AreaSource? GuestHouseAreaTotalSource { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<AreaUnits?>))]
        public AreaUnits? GuestHouseAreaTotalUnits { get; set; }

        public string GuestHouseDescription { get; set; }

        public bool? GuestHouseYN { get; set; }

        [JsonConverter(typeof(StringListEnumConverter<GulfAccessType>))]
        public IEnumerable<GulfAccessType> GulfAccessType { get; set; }

        public bool? GulfAccessYN { get; set; }

        public bool? HumanModifiedYN { get; set; }

        public bool? InternetEntireListingDisplayYN { get; set; }

        public string LakeChainName { get; set; }

        public string LakeId { get; set; }

        public string LakeName { get; set; }

        public string LakeSize { get; set; }

        [JsonConverter(typeof(StringListEnumConverter<LandTenure>))]
        public IEnumerable<LandTenure> LandTenure { get; set; }

        public string Lang2_Type { get; set; }

        public string Lang3_Type { get; set; }

        public bool? LastMonthRentReqYN { get; set; }

        public decimal? LeaseAmountPerArea { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<LeaseAmountPerAreaUnit?>))]
        public LeaseAmountPerAreaUnit? LeaseAmountPerAreaUnit { get; set; }

        public string LeaseTermsDescription { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<AOR?>))]
        public AOR? ListAOR { get; set; }

        public string ListingId { get; set; }

        public string ListingKey { get; set; }

        public long? ListingKeyNumeric { get; set; }

        public string ListOfficeKey { get; set; }

        public string ListOfficeMlsId { get; set; }

        public string LivingAreaRange { get; set; }

        public decimal? LivingAreaRangeHigh { get; set; }

        public decimal? LivingAreaRangeLow { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<AreaSource?>))]
        public AreaSource? LivingAreaRangeSource { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<AreaUnits?>))]
        public AreaUnits? LivingAreaRangeUnits { get; set; }

        public string Location { get; set; }

        public decimal? LotSizeAreaRangeHigh { get; set; }

        public decimal? LotSizeAreaRangeLow { get; set; }

        public string LotSizeRange { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<LotSizeSource?>))]
        public LotSizeSource? LotSizeRangeSource { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<LotSizeUnits?>))]
        public LotSizeUnits? LotSizeRangeUnits { get; set; }

        [JsonConverter(typeof(StringListEnumConverter<Membership>))]
        public IEnumerable<Membership> Membership { get; set; }

        public decimal? MembershipFee { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<FeeFrequency?>))]
        public FeeFrequency? MembershipFeeFrequency { get; set; }

        public bool? MembershipRequiredYN { get; set; }

        [JsonConverter(typeof(StringListEnumConverter<MineralRights>))]
        public IEnumerable<MineralRights> MineralRights { get; set; }

        public DateTimeOffset? ModificationTimestamp { get; set; }

        public string MonthlyRate { get; set; }

        public int? NumberOfBoatDocks { get; set; }

        public int? NumberOfBoatSlips { get; set; }

        public string OffersDescription { get; set; }

        public DateTime? OffersReviewDate { get; set; }

        public DateTime? OffMarketDate { get; set; }

        public string OffSeasonRate { get; set; }

        public string OriginatingSystemKey { get; set; }

        public string OriginatingSystemName { get; set; }

        public string OriginatingSystemSubName { get; set; }

        public string OtherExpenseDescription { get; set; }

        [JsonConverter(typeof(StringListEnumConverter<ListingPermission>))]
        public IEnumerable<ListingPermission> Permission { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<PotentialShortSale?>))]
        public PotentialShortSale? PotentialShortSale { get; set; }

        public decimal? PricePerArea { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<PricePerAreaUnit?>))]
        public PricePerAreaUnit? PricePerAreaUnit { get; set; }

        public string PrivateShowingInstructions { get; set; }

        public string ProjectName { get; set; }

        [JsonConverter(typeof(StringListEnumConverter<PropertyAccess>))]
        public IEnumerable<PropertyAccess> PropertyAccess { get; set; }

        public string PublicRemarks_lang2 { get; set; }

        public string PublicRemarks_lang3 { get; set; }

        public string RentSpreeURL { get; set; }

        public bool? RentSpreeYN { get; set; }

        [JsonConverter(typeof(StringListEnumConverter<Restrictions>))]
        public IEnumerable<Restrictions> Restrictions { get; set; }

        public string RiverName { get; set; }

        public string SaleOrLeaseIncludes { get; set; }

        public string SeasonRate { get; set; }

        public string SecurityDepositDescription { get; set; }

        public bool? SecurityDepositYN { get; set; }

        public int? SourceSupplementPublicCount { get; set; }

        public string SourceSystemKey { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<StandardStatus?>))]
        public StandardStatus? StandardStatus { get; set; }

        public string StoriesPartial { get; set; }

        public string StoriesPartialTotal { get; set; }

        [JsonConverter(typeof(StringListEnumConverter<StormProtection>))]
        public IEnumerable<StormProtection> StormProtection { get; set; }

        public string TaxAssessedValueImprovement { get; set; }

        public string TaxAssessedValueLand { get; set; }

        public string TaxAuthority { get; set; }

        public decimal? TaxRate { get; set; }

        public string TaxYearRange { get; set; }

        [JsonConverter(typeof(StringListEnumConverter<ThirdPartyIntegrationType>))]
        public IEnumerable<ThirdPartyIntegrationType> ThirdPartyIntegrationType { get; set; }

        public string TitleCompanyAddress { get; set; }

        public string TitleCompanyName { get; set; }

        public string TitleCompanyPhone { get; set; }

        public string TitleCompanyPreferred { get; set; }

        public string UnitLocation { get; set; }

        public string WaterAccessDescription { get; set; }

        public bool? WaterAccessYN { get; set; }

        public string WeeklyRate { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<PropertyType?>))]
        public PropertyType? PropertyType { get; set; }

        public IEnumerable<Property> Property { get; set; }
    }
}
