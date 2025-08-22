namespace Husa.Extensions.Downloader.Trestle.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using Husa.Extensions.Downloader.Trestle.Helpers.Converters;
    using Husa.Extensions.Downloader.Trestle.Models.Enums;

    public class PropertyRooms
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

        public decimal? RoomArea { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<AreaSource?>))]
        public AreaSource? RoomAreaSource { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<AreaUnits?>))]
        public AreaUnits? RoomAreaUnits { get; set; }

        public string RoomDescription { get; set; }

        public string RoomDimensions { get; set; }

        [JsonConverter(typeof(StringListEnumConverter<InteriorOrRoomFeatures>))]
        public IEnumerable<InteriorOrRoomFeatures> RoomFeatures { get; set; }

        [JsonConverter(typeof(StringListEnumConverter<Flooring>))]
        public IEnumerable<Flooring> RoomFlooring { get; set; }

        public string RoomKey { get; set; }

        public int? RoomKeyNumeric { get; set; }

        public decimal? RoomLength { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<AreaSource?>))]
        public AreaSource? RoomLengthWidthSource { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<LinearUnits?>))]
        public LinearUnits? RoomLengthWidthUnits { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<RoomLevel?>))]
        public RoomLevel? RoomLevel { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<RoomType?>))]
        public RoomType? RoomType { get; set; }

        public decimal? RoomWidth { get; set; }

        public string SourceSystemID { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<StandardStatus?>))]
        public StandardStatus? StandardStatus { get; set; }

        [JsonConverter(typeof(StringListEnumConverter<SyndicateTo>))]
        public IEnumerable<SyndicateTo> SyndicateTo { get; set; }

        public IEnumerable<Property> Property { get; set; }
    }
}
