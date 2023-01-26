namespace Husa.Extensions.Downloader.Trestle.Contracts
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class ODataResponse<T>
    {
        [JsonPropertyName("@odata.metadata")]
        public string Metadata { get; set; }

        [JsonPropertyName("@odata.context")]
        public string Context { get; set; }

        [JsonPropertyName("@odata.nextLink")]
        public string NextLink { get; set; }

        [JsonPropertyName("@odata.Count")]
        public int Count { get; set; }
        
        [JsonPropertyName("value")]
        public IEnumerable<T> Value { get; set; }
    }
}
