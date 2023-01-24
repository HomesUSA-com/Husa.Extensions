namespace Husa.Extensions.Downloader.Trestle.Models.Enums
{
    using System.Text.Json.Serialization;

    public enum AvailabilityType
    {
        [JsonPropertyName("Annual")]
        Annual,
        [JsonPropertyName("Biweekly")]
        Biweekly,
        [JsonPropertyName("Daily")]
        Daily,
        [JsonPropertyName("Long Term")]
        LongTerm,
        [JsonPropertyName("Monthly")]
        Monthly,
        [JsonPropertyName("None")]
        None,
        [JsonPropertyName("Off-Season")]
        OffSeason,
        [JsonPropertyName("Other")]
        Other,
        [JsonPropertyName("Seasonal")]
        Seasonal,
        [JsonPropertyName("Short Term")]
        ShortTerm,
        [JsonPropertyName("Weekly")]
        Weekly,
    }
}
