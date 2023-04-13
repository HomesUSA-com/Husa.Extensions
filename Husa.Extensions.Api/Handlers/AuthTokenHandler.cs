namespace Husa.Extensions.Api.Handlers
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Husa.Extensions.Api.Client;
    using IdentityModel.Client;
    using Microsoft.Extensions.Options;

    public class AuthTokenHandler : DelegatingHandler
    {
        private readonly IdentitySettings identityOptions;
        private readonly HttpClient httpClient;

        public AuthTokenHandler(IOptions<IdentitySettings> identityOptions, HttpClient httpClient)
        {
            this.identityOptions = identityOptions is null ? throw new ArgumentNullException(nameof(identityOptions)) : identityOptions.Value;
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Headers.Authorization == null)
            {
                var accessToken = await this.GetPasswordTokenString();
                request.Headers.Add(HttpClientExtensions.AuthorizationHeaderName, $"{HttpClientExtensions.Bearer} {accessToken}");
            }

            return await base.SendAsync(request, cancellationToken);
        }

        private async Task<string> GetPasswordTokenString()
        {
            var tokenRequest = new PasswordTokenRequest
            {
                Address = this.identityOptions.Uri,
                ClientId = this.identityOptions.ClientId,
                ClientSecret = this.identityOptions.ClientSecret,
                Scope = this.identityOptions.Scope,
                UserName = this.identityOptions.UserName,
                Password = this.identityOptions.Password,
            };

            // request the access token token
            var tokenResponse = await this.httpClient.RequestPasswordTokenAsync(tokenRequest);
            if (tokenResponse.IsError)
            {
                throw new HttpRequestException(tokenResponse.Error);
            }

            return tokenResponse.AccessToken;
        }
    }
}
