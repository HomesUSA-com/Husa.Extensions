namespace Husa.Extensions.OpenAI.Tests.Helpers
{
    using System.Collections.Generic;
    using Husa.Extensions.OpenAI.Helpers;
    using Xunit;

    public class PropertiesExtensionsTests
    {
        [Fact]
        public void IsEmptyCollections_WithEmptyStringList_ReturnsTrue()
        {
            // Arrange
            var emptyList = new List<string>();

            // Act
            var result = emptyList.IsEmptyCollections();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsEmptyCollections_WithNonEmptyStringList_ReturnsFalse()
        {
            // Arrange
            var list = new List<string> { "item1" };

            // Act
            var result = list.IsEmptyCollections();

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void IsNullOrEmpty_WithNullOrEmptyString_ReturnsTrue(string value)
        {
            // Act
            var result = value.IsNullOrEmpty();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsNullOrEmpty_WithNonEmptyString_ReturnsFalse()
        {
            // Arrange
            var value = "test";

            // Act
            var result = value.IsNullOrEmpty();

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(0)]
        [InlineData(0.0)]
        public void IsNullOrZero_WithNullOrZeroDecimal_ReturnsTrue(object value)
        {
            // Act
            var result = value.IsNullOrZero();

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(1.1)]
        [InlineData(-1.1)]
        [InlineData(1)]
        [InlineData(-1)]
        public void IsNullOrZero_WithNonZeroDecimal_ReturnsFalse(object value)
        {
            // Act
            var result = value.IsNullOrZero();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsNullOrZero_WithNonNumericType_ReturnsFalse()
        {
            // Arrange
            object value = "test";

            // Act
            var result = value.IsNullOrZero();

            // Assert
            Assert.False(result);
        }
    }
}
