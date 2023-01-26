namespace Husa.Extensions.Downloader.Trestle.Models
{
    using System;
    using Husa.Extensions.Downloader.Trestle.Models.Enums;

    public class PropertyRooms
    {
        public bool HumanModifiedYN { get; set; }
        public string ListAgentKey { get; set; }
        public string ListingId { get; set; }
        public string ListingKey { get; set; }
        public int ListingKeyNumeric { get; set; }
        public string ListOfficeKey { get; set; }
        public DateTimeOffset ModificationTimestamp { get; set; }
        public string OriginatingSystemName { get; set; }
        public string OriginatingSystemSubName { get; set; }
        public Permission? Permission { get; set; }
        public string PermissionPrivate { get; set; }
        public decimal RoomArea { get; set; }
        public AreaSource? RoomAreaSource { get; set; }
        public AreaUnits? RoomAreaUnits { get; set; }
        public string RoomDescription { get; set; }
        public string RoomDimensions { get; set; }
        public InteriorOrRoomFeatures? RoomFeatures { get; set; }
        public Flooring? RoomFlooring { get; set; }
        public string RoomKey { get; set; }
        public int RoomKeyNumeric { get; set; }
        public decimal RoomLength { get; set; }
        public AreaSource? RoomLengthWidthSource { get; set; }
        public LinearUnits? RoomLengthWidthUnits { get; set; }
        public RoomLevel? RoomLevel { get; set; }
        public RoomType? RoomType { get; set; }
        public decimal RoomWidth { get; set; }
    }
}
