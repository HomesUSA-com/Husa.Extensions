namespace Husa.Extensions.Api
{
    using AutoMapper;
    using AutoMapper.Configuration;

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

        public static IMapperConfigurationExpression AddMapping<T>(this IMapperConfigurationExpression config)
            where T : class
        {
            config.AddMaps(typeof(T));

            return config;
        }
    }
}
