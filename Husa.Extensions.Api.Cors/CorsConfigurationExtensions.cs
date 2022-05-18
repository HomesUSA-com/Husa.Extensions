namespace Husa.Extensions.Api.Cors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    public static class CorsConfigurationExtensions
    {
        private const string DefaultCorsPolicyName = "_myAllowSpecificOrigins";

        public static void ConfigureCors(this IApplicationBuilder app, string corsPolicyName = DefaultCorsPolicyName)
        {
            app.UseCors(corsPolicyName);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers()
                    .RequireCors(corsPolicyName);
            });
        }

        public static IServiceCollection RegisterCors(
            this IServiceCollection services,
            IEnumerable<string> origins,
            string corsPolicyName = DefaultCorsPolicyName,
            IEnumerable<string> allowedMethods = null)
        {
            if (!origins.Any())
            {
                throw new ArgumentException("You must provide at least one origin", nameof(origins));
            }

            if (allowedMethods == null || !allowedMethods.Any())
            {
                allowedMethods = new[]
                {
                    "PUT", "DELETE", "GET", "POST", "PATCH",
                };
            }

            return services.AddCors(options =>
            {
                options.AddPolicy(
                    name: corsPolicyName,
                    builder =>
                    {
                        builder
                            .WithOrigins(origins.ToArray())
                            .AllowAnyHeader()
                            .AllowCredentials()
                            .WithMethods(allowedMethods.ToArray());
                    });
            });
        }
    }
}
