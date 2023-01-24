namespace Husa.Extensions.Downloader.Trestle.Models.Enums
{
    using System.Text.Json.Serialization;

    public enum AreaUnits
    {
        [JsonPropertyName("Square Feet")]
        SquareFeet,
        [JsonPropertyName("Square Meters")]
        SquareMeters,
    }
}
