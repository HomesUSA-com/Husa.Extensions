namespace Husa.Extensions.Api
{
    using Husa.Extensions.Api.Conventions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;

    public static class Configuration
    {
        public static MvcOptions AddControllerPrefixConventions(this MvcOptions options,string prefix, params string[] excluded)
        {
            options.Conventions.Add(new RoutePrefixConvention(prefix, excluded));

            return options;

        }
    }
}
