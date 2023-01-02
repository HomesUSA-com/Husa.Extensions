namespace Husa.Extensions.Api.Client
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using IdentityModel.Client;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Net.Http.Headers;

    public static class HttpClientExtensions
    {
        public const string Bearer = "Bearer";
        public const string AccessToken = "access_token";
        public const string CurrentCompanyHeaderName = "CurrentCompanySelected";
        public const string CookieHeaderName = "Cookie";
        public const string AuthorizationHeaderName = "Authorization";

        public static IServiceCollection ConfigureHeaderPropagation(this IServiceCollection services)
        {
            services.AddHeaderPropagation(options =>
            {
                options.Headers.Add(AuthorizationHeaderName);
                options.Headers.Add(CookieHeaderName);
                options.Headers.Add(CurrentCompanyHeaderName);
            });

            return services;
        }

        public static async Task<string> GetPasswordToken(this HttpClient httpClient, PasswordTokenRequest tokenRequest)
        {
            // request the access token token
            var tokenResponse = await httpClient.RequestPasswordTokenAsync(tokenRequest);
            if (tokenResponse.IsError)
            {
                throw new HttpRequestException(tokenResponse.Error);
            }

            return tokenResponse.AccessToken;
        }

        public static void AddBearerToken(this HttpClient httpClient, string authToken)
        {
            if (httpClient is null)
            {
                throw new ArgumentNullException(nameof(httpClient), "Cannot add an authorization bearer token to a null client");
            }

            if (string.IsNullOrWhiteSpace(authToken))
            {
                return;
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Bearer, authToken);
        }

        public static string GetBearerToken(this HttpContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            string authHeader = context.Request.Headers[HeaderNames.Authorization];

            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith(Bearer))
            {
                var tokenNameLength = Bearer.Length + 1;
                return authHeader[tokenNameLength..].Trim();
            }

            return string.Empty;
        }

        public static bool IsValidToken(this string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            var jwtToken = new JwtSecurityToken(token);
            return jwtToken.ValidTo >= DateTime.UtcNow.AddMinutes(-1);
        }
    }
}
