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
            if (mapperConfigExpression is null)
            {
                throw new ArgumentNullException(nameof(mapperConfigExpression));
            }

            mapperConfigExpression.AddMaps(typeof(T));

            return mapperConfigExpression;
        }
    }
}
