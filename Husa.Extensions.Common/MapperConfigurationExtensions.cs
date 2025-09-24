namespace Husa.Extensions.Common
{
    using System;
    using AutoMapper;

    public static class MapperConfigurationExtensions
    {
        public static IMapperConfigurationExpression Configure => new MapperConfigurationExpression();

        public static IConfigurationProvider Build(this IMapperConfigurationExpression configExpression)
        {
            var config = new MapperConfiguration((MapperConfigurationExpression)configExpression);
            config.AssertConfigurationIsValid();
            config.CompileMappings();
            return config;
        }

        public static IMapperConfigurationExpression AddMapping<T>(this IMapperConfigurationExpression mapperConfigExpression)
            where T : class
        {
            ArgumentNullException.ThrowIfNull(mapperConfigExpression);

            mapperConfigExpression.AddMaps(typeof(T));

            return mapperConfigExpression;
        }

        public static IMapperConfigurationExpression AddMappingProfile<T>(this IMapperConfigurationExpression mapperConfigExpression)
            where T : Profile
        {
            ArgumentNullException.ThrowIfNull(mapperConfigExpression);

            mapperConfigExpression.AddProfile(typeof(T));

            return mapperConfigExpression;
        }
    }
}
