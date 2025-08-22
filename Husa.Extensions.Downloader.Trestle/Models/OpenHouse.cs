namespace Husa.Extensions.Downloader.Trestle.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using Husa.Extensions.Downloader.Trestle.Helpers.Converters;
    using Husa.Extensions.Downloader.Trestle.Models.Enums;

    public class OpenHouse
    {
        public bool? AppointmentRequiredYN { get; set; }

        public bool? HumanModifiedYN { get; set; }

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

        public string LivestreamOpenHouseURL { get; set; }

        public DateTimeOffset? ModificationTimestamp { get; set; }

        public DateTime? OffMarketDate { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<Attended?>))]
        public Attended? OpenHouseAttendedBy { get; set; }

        public DateTime? OpenHouseDate { get; set; }

        public DateTimeOffset? OpenHouseEndTime { get; set; }

        public string OpenHouseId { get; set; }

        public string OpenHouseKey { get; set; }

        public int? OpenHouseKeyNumeric { get; set; }

        public string OpenHouseRemarks { get; set; }

        public DateTimeOffset? OpenHouseStartTime { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<OpenHouseStatus?>))]
        public OpenHouseStatus? OpenHouseStatus { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<OpenHouseType?>))]
        public OpenHouseType? OpenHouseType { get; set; }

        public DateTimeOffset? OriginalEntryTimestamp { get; set; }

        public string OriginatingSystemID { get; set; }

        public string OriginatingSystemKey { get; set; }

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

        public string Refreshments { get; set; }

        public string ShowingAgentFirstName { get; set; }

        public string ShowingAgentKey { get; set; }

        public int? ShowingAgentKeyNumeric { get; set; }

        public string ShowingAgentLastName { get; set; }

        public string ShowingAgentMlsID { get; set; }

        public string SourceSystemID { get; set; }

        public string SourceSystemKey { get; set; }

        public string SourceSystemListingKey { get; set; }

        public string SourceSystemName { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<StandardStatus?>))]
        public StandardStatus? StandardStatus { get; set; }

        [JsonConverter(typeof(StringListEnumConverter<SyndicateTo>))]
        public IEnumerable<SyndicateTo> SyndicateTo { get; set; }

        public IEnumerable<Property> Property { get; set; }
    }
}
