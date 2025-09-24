namespace Husa.Extensions.Common.Tests
{
    using System;
    using AutoMapper;
    using Husa.Extensions.Common.Tests.Providers;
    using Xunit;

    public class MapperConfigurationExtensionsTests
    {
        [Fact]
        public void AddMappingProfile_AddsProfileSuccessfully()
        {
            // Arrange
            var configExpression = new MapperConfigurationExpression();

            // Act
            configExpression.AddMappingProfile<TestProfile>();
            var mapperConfig = new MapperConfiguration(configExpression);

            // Assert
            mapperConfig.AssertConfigurationIsValid();
            var mapper = mapperConfig.CreateMapper();
            var source = new Source { Value = 42 };
            var dest = mapper.Map<Destination>(source);
            Assert.Equal(source.Value, dest.Value);
        }

        [Fact]
        public void AddMappingProfile_NullConfig_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                MapperConfigurationExtensions.AddMappingProfile<TestProfile>(null));
        }
    }
}
