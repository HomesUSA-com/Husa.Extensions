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
        public string ListAgentKey { get; set; }
        public string ListingId { get; set; }
        public string ListingKey { get; set; }
        public int? ListingKeyNumeric { get; set; }
        public string ListOfficeKey { get; set; }
        public DateTimeOffset ModificationTimestamp { get; set; }
        public string OriginatingSystemName { get; set; }
        public string OriginatingSystemSubName { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<Permission?>))]
        public Permission? Permission { get; set; }
        public string PermissionPrivate { get; set; }
        public decimal? RoomArea { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<AreaSource?>))]
        public AreaSource? RoomAreaSource { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<AreaUnits?>))]
        public AreaUnits? RoomAreaUnits { get; set; }
        public string RoomDescription { get; set; }
        public string RoomDimensions { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<InteriorOrRoomFeatures>))]
        public IEnumerable<InteriorOrRoomFeatures> RoomFeatures { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<Flooring?>))]
        public Flooring? RoomFlooring { get; set; }
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
    }
}
