namespace Husa.Extensions.Downloader.Trestle.Models.Enums
{
    using System.Text.Json.Serialization;

    public enum AreaSource
    {
        [JsonPropertyName("Appraiser")]
        Appraiser,
        [JsonPropertyName("Assessor")]
        Assessor,
        [JsonPropertyName("Builder")]
        Builder,
        [JsonPropertyName("Condo Documents")]
        CondoDocuments,
        [JsonPropertyName("Estimated")]
        Estimated,
        [JsonPropertyName("GIS Calculated")]
        GISCalculated,
        [JsonPropertyName("Listing Agent")]
        ListingAgent,
        [JsonPropertyName("Measured")]
        Measured,
        [JsonPropertyName("Multiple")]
        Multiple,
        [JsonPropertyName("Not Available")]
        NotAvailable,
        [JsonPropertyName("Not Measured")]
        NotMeasured,
        [JsonPropertyName("Other")]
        Other,
        [JsonPropertyName("Owner")]
        Owner,
        [JsonPropertyName("Plans")]
        Plans,
        [JsonPropertyName("Public Records")]
        PublicRecords,
        [JsonPropertyName("Realist")]
        Realist,
        [JsonPropertyName("See Remarks")]
        SeeRemarks,
        [JsonPropertyName("Survey")]
        Survey,
    }
}
