namespace Husa.Extensions.Downloader.Trestle.Contracts
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class ODataResponse<T>
    {
        [JsonProperty("@odata.metadata")]
        public string Metadata { get; set; }

        [JsonProperty("@odata.context")]
        public string Context { get; set; }

        [JsonProperty("@odata.nextLink")]
        public string NextLink { get; set; }

        [JsonProperty("@odata.Count")]
        public int Count { get; set; }

        public IEnumerable<T> Value { get; set; }
    }
}
