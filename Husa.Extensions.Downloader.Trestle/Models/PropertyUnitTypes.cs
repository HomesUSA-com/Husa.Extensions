namespace Husa.Extensions.Downloader.Trestle.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using Husa.Extensions.Downloader.Trestle.Helpers.Converters;
    using Husa.Extensions.Downloader.Trestle.Models.Enums;

    public class PropertyUnitTypes
    {
        public bool? HumanModifiedYN { get; set; }

        public int? InputEntryOrder { get; set; }

        public bool? InternetEntireListingDisplayYN { get; set; }

        public string ListAgentKey { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<AOR?>))]
        public AOR? ListAOR { get; set; }

        public string ListingId { get; set; }

        public string ListingKey { get; set; }

        public long? ListingKeyNumeric { get; set; }

        [JsonConverter(typeof(StringListEnumConverter<ListingPermission>))]
        public IEnumerable<ListingPermission> ListingPermission { get; set; }

        public string ListOfficeKey { get; set; }

        public string ListOfficeMlsId { get; set; }

        public DateTimeOffset? ModificationTimestamp { get; set; }

        public DateTime? OffMarketDate { get; set; }

        public string OriginatingSystemListingKey { get; set; }

        public string OriginatingSystemName { get; set; }

        public string OriginatingSystemSubName { get; set; }

        [JsonConverter(typeof(StringListEnumConverter<ListingPermission>))]
        public IEnumerable<ListingPermission> Permission { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<PropertySubType?>))]
        public PropertySubType? PropertySubType { get; set; }

        [JsonConverter(typeof(StringListEnumConverter<PropertySubTypeAdditional>))]
        public IEnumerable<PropertySubTypeAdditional> PropertySubTypeAdditional { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<PropertyType?>))]
        public PropertyType? PropertyType { get; set; }

        public int? RecordSignature { get; set; }

        public string SourceSystemID { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<StandardStatus?>))]
        public StandardStatus? StandardStatus { get; set; }

        [JsonConverter(typeof(StringListEnumConverter<SyndicateTo>))]
        public IEnumerable<SyndicateTo> SyndicateTo { get; set; }

        public decimal? UnitTypeActualRent { get; set; }

        public string UnitTypeActualRentRange { get; set; }

        public decimal? UnitTypeArea { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<AreaSource?>))]
        public AreaSource? UnitTypeAreaSource { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<AreaUnits?>))]
        public AreaUnits? UnitTypeAreaUnits { get; set; }

        public int? UnitTypeBathsTotal { get; set; }

        public int? UnitTypeBedsTotal { get; set; }

        public decimal? UnitTypeDeposit { get; set; }

        public string UnitTypeDescription { get; set; }

        public bool? UnitTypeFireplaceYN { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<Furnished?>))]
        public Furnished? UnitTypeFurnished { get; set; }

        public bool? UnitTypeGarageAttachedYN { get; set; }

        public decimal? UnitTypeGarageSpaces { get; set; }

        public string UnitTypeKey { get; set; }

        public int? UnitTypeKeyNumeric { get; set; }

        public bool? UnitTypeLeasedYN { get; set; }

        public DateTimeOffset? UnitTypeLeaseExpires { get; set; }

        public bool? UnitTypeMonthToMonthYN { get; set; }

        public int? UnitTypeNumFullBaths { get; set; }

        public int? UnitTypeNumHalfBaths { get; set; }

        [JsonConverter(typeof(StringListEnumConverter<UnitTypeOccupantType>))]
        public IEnumerable<UnitTypeOccupantType> UnitTypeOccupantType { get; set; }

        public decimal? UnitTypePetDeposit { get; set; }

        public bool? UnitTypePetDepositPerPetYN { get; set; }

        public int? UnitTypeProForma { get; set; }

        public decimal? UnitTypeTotalRent { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<UnitTypeType?>))]
        public UnitTypeType? UnitTypeType { get; set; }

        public string UnitTypeUnitNum { get; set; }

        public int? UnitTypeUnitsTotal { get; set; }

        public IEnumerable<Property> Property { get; set; }
    }
}
