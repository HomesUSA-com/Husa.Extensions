namespace Husa.Extensions.Common.Tests
{
    using Xunit;
    public class StringExtensionsTests
    {
        [Fact]
        public void ToTitleCase_Success()
        {
            // Arrange
            var str = "TEST TITLE";

            // Act
            var result = str.ToTitleCase();

            // Assert
            Assert.Equal("Test Title", result);
        }
    }
}
