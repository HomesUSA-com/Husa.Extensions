namespace Husa.Extensions.Downloader.Trestle.Models.Enums
{
    using System.Text.Json.Serialization;

    public enum BoatDockSlipFeatures
    {
        [JsonPropertyName("Community Ramp")]
        CommunityRamp,
        [JsonPropertyName("Deep Water Access")]
        DeepWaterAccess,
        [JsonPropertyName("Dock Available")]
        DockAvailable,
        [JsonPropertyName("Hoist")]
        Hoist,
        [JsonPropertyName("Lift")]
        Lift,
        [JsonPropertyName("Marina Services")]
        MarinaServices,
        [JsonPropertyName("Marine Rail")]
        MarineRail,
        [JsonPropertyName("Ramp")]
        Ramp,
        [JsonPropertyName("See Agent")]
        SeeAgent,
        [JsonPropertyName("Slip Available")]
        SlipAvailable,
    }
}
