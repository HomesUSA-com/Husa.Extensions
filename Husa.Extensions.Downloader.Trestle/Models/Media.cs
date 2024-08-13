namespace Husa.Extensions.Downloader.Trestle.Models
{
    using System;
    using System.IO;
    using System.Text.Json.Serialization;
    using Husa.Extensions.Downloader.Trestle.Helpers.Converters;
    using Husa.Extensions.Downloader.Trestle.Models.Enums;

    public class Media
    {
        public string ChangedByMemberID { get; set; }
        public string ChangedByMemberKey { get; set; }
        public int? ChangedByMemberKeyNumeric { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<ClassName?>))]
        public ClassName? ClassName { get; set; }
        public bool? HumanModifiedYN { get; set; }
        public int? ImageHeight { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<ImageOf?>))]
        public ImageOf? ImageOf { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<ImageSizeDescription?>))]
        public ImageSizeDescription? ImageSizeDescription { get; set; }
        public int? ImageWidth { get; set; }
        public bool? InternetEntireListingDisplayYN { get; set; }
        public string ListAgentKey { get; set; }
        public AOR ListAOR { get; set; }
        public string ListingPermission { get; set; }
        public string ListOfficeKey { get; set; }
        public string ListOfficeMlsId { get; set; }
        public string LongDescription { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<MediaAlteration?>))]
        public MediaAlteration? MediaAlteration { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<MediaCategory?>))]
        public MediaCategory? MediaCategory { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<MediaClassification?>))]
        public MediaClassification? MediaClassification { get; set; }
        public string MediaHTML { get; set; }
        public string MediaKey { get; set; }
        public long? MediaKeyNumeric { get; set; }
        public DateTimeOffset MediaModificationTimestamp { get; set; }
        public string MediaObjectID { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<MediaStatus?>))]
        public MediaStatus? MediaStatus { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<MediaType?>))]
        public MediaType? MediaType { get; set; }
        public string MediaURL { get; set; }
        public DateTimeOffset ModificationTimestamp { get; set; }
        public DateTime? OffMarketDate { get; set; }
        public int? Order { get; set; }
        public string OriginatingSystemID { get; set; }
        public string OriginatingSystemMediaKey { get; set; }
        public string OriginatingSystemName { get; set; }
        public string OriginatingSystemResourceRecordId { get; set; }
        public string OriginatingSystemResourceRecordKey { get; set; }
        public string OriginatingSystemSubName { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<Permission?>))]
        public Permission? Permission { get; set; }
        public string PermissionPrivate { get; set; }
        public bool? PreferredPhotoYN { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<PropertySubType?>))]
        public PropertySubType? PropertySubType { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<PropertySubTypeAdditional?>))]
        public PropertySubTypeAdditional? PropertySubTypeAdditional { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<PropertyType?>))]
        public PropertyType? PropertyType { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<ResourceName?>))]
        public ResourceName? ResourceName { get; set; }
        public string ResourceRecordID { get; set; }
        public string ResourceRecordKey { get; set; }
        public int? ResourceRecordKeyNumeric { get; set; }
        public string ShortDescription { get; set; }
        public string SourceSystemID { get; set; }
        public string SourceSystemMediaKey { get; set; }
        public string SourceSystemName { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<StandardStatus?>))]
        public StandardStatus? StandardStatus { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<SyndicateTo?>))]
        public SyndicateTo? SyndicateTo { get; set; }
        public Stream X_MediaStream { get; set; }
    }
}
