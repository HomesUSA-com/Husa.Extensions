namespace Husa.Extensions.Api
{
    using System;
    using System.Linq;
    using Husa.Extensions.Api.Configuration;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class IdentityApiConfiguration
    {
        private static ApplicationOptions settings;

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

            if (settings.Policies.Claims.Any())
            {
                foreach (var claim in settings.Policies.Claims)
                {
                    services.AddAuthorization(o => o.AddPolicy(
                        claim.Name,
                        p => p.RequireClaim(claim.Type, claim.AllowedValues)));
                }
            }

            if (settings.Policies.Roles.Any())
            {
                foreach (var role in settings.Policies.Roles)
                {
                    services.AddAuthorization(o => o.AddPolicy(
                        role.Name,
                        p => p.RequireRole(role.RolesClaim)));
                }
            }

            return services;
        }

        public static void GetSettings(IConfiguration configuration)
        {
            var applicationOptions = new ApplicationOptions();
            configuration.GetSection(ApplicationOptions.Section).Bind(applicationOptions);
            settings = applicationOptions;
        }
    }
}
