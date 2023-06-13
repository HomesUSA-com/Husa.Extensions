namespace Husa.Extensions.Api.Cors
{
    using System.Globalization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Localization;

    public static class CultureAndLocalizationConfigurationExtensions
    {
        public static void ConfigureCultureAndLocalization(this IApplicationBuilder app)
        {
            var enUsCulture = new CultureInfo("en-US");
            var localizationOptions = new RequestLocalizationOptions()
            {
                SupportedCultures = new[] { enUsCulture },
                SupportedUICultures = new[] { enUsCulture },
                DefaultRequestCulture = new RequestCulture(enUsCulture),
                FallBackToParentCultures = false,
                FallBackToParentUICultures = false,
                RequestCultureProviders = null,
            };

            app.UseRequestLocalization(localizationOptions);
            CultureInfo.DefaultThreadCurrentCulture = enUsCulture;
        }
    }
}
