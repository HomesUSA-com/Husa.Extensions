namespace Husa.Extensions.OpenAI.Providers
{
    using System.ClientModel;
    using global::OpenAI.Chat;
    using Husa.Extensions.OpenAI.Options;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    public class TestOpenAIClient : OpenAIClient
    {
        public TestOpenAIClient(
            ChatClient client,
            IOptions<OpenAIOptions> options,
            ILogger<OpenAIClient> logger)
            : base(client, options, logger)
        {
        }

        internal override string GetDescription(ClientResult<ChatCompletion> chatResult)
        {
            return @"
            {
                ""images"": [
                    {
                        ""contains_branding"": true,
                        ""contains_persons"": false,
                        ""explanation"": ""This image contains branding.""
                    },
                    {
                        ""contains_branding"": false,
                        ""contains_persons"": true,
                        ""explanation"": ""This image contains persons.""
                    }
                ]
            }";
        }
    }
}
