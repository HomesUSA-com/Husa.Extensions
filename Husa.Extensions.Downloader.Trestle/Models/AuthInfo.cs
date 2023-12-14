namespace Husa.Extensions.Downloader.Trestle.Models
{
    public class AuthInfo
    {
        public AuthInfo(string clientId, string clientSecret, string partitionKey)
        {
            this.ClientId = clientId;
            this.ClientSecret = clientSecret;
            this.PartitionKey = partitionKey;
        }

        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string PartitionKey { get; set; }
    }
}
