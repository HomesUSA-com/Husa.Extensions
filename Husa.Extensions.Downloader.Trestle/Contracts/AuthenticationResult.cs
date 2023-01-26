namespace Husa.Extensions.Downloader.Trestle.Contracts
{
    using System.Text.Json.Serialization;

    public class AuthenticationResult
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }
    }
}
