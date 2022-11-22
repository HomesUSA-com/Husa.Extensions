namespace Husa.Extensions.Api
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Husa.Extensions.Api.Client;
    using Husa.Extensions.Api.Configuration;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class IdentityApiConfiguration
    {
        private static ApiOptions settings;

        public static IServiceCollection ConfigureApiClients(this IServiceCollection services, IConfiguration configuration)
        {
            GetSettings(configuration);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = settings.ApiConfiguration.Authority;
                    options.Audience = settings.ApiConfiguration.ApiName;
                    options.TokenValidationParameters.ClockSkew = TimeSpan.Zero;
                    options.TokenValidationParameters.ValidateLifetime = true;
                });

            if (settings.Policies == null)
            {
                return services;
            }

            foreach (var claim in settings.Policies.Claims)
            {
                services.AddAuthorization(o => o.AddPolicy(
                    claim.Name,
                    p => p.RequireClaim(claim.Type, claim.AllowedValues)));
            }

            foreach (var role in settings.Policies.Roles)
            {
                services.AddAuthorization(o => o.AddPolicy(
                    role.Name,
                    p => p.RequireRole(role.RolesClaim)));
            }

            return services;
        }

        public static async Task ConfigureClientAsync(
            this HttpClient client,
            IServiceProvider provider,
            string baseAddress,
            string tokenName = HttpClientExtensions.AccessToken,
            string authenticationHeaderScheme = HttpClientExtensions.Bearer)
        {
            var httpContextAccessor = provider.GetService<IHttpContextAccessor>();
            if (httpContextAccessor.HttpContext != null)
            {
                var accessToken = await httpContextAccessor.HttpContext.GetTokenAsync(tokenName);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authenticationHeaderScheme, accessToken);

                if (Guid.TryParse(httpContextAccessor.HttpContext.Request.Headers[HttpClientExtensions.CurrentCompanyHeaderName], out var currentCompanyId))
                {
                    client.DefaultRequestHeaders.Add(HttpClientExtensions.CurrentCompanyHeaderName, currentCompanyId.ToString());
                }
            }

            client.BaseAddress = new Uri(baseAddress);
        }

        public static void GetSettings(IConfiguration configuration)
        {
            var apiOptions = new ApiOptions();
            configuration.GetSection(ApiOptions.Section).Bind(apiOptions);
            settings = apiOptions;
        }
    }
}
