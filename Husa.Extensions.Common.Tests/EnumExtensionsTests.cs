namespace Husa.Extensions.Common.Tests
{
    using System;
    using System.ComponentModel;
    using System.Runtime.Serialization;
    using Xunit;

    internal enum EnumTestDescription
    {
        [EnumMember(Value = "ATT")]
        [Description("Attached")]
        Attached,
    }

    public class EnumExtensionsTests
    {
        [Theory]
        [InlineData("ATT")]
        [InlineData("att")]
        public void ToEnumFromEnumMember_Success(string value)
        {
            // Arrange
            // Act
            var result = value.ToEnumFromEnumMember<EnumTestDescription>();

            // Assert
            Assert.Equal(EnumTestDescription.Attached, result);
        }

        [Fact]
        public void ToEnumOrNullFromEnumMember_Null()
        {
            // Arrange
            // Act
            var result = "other".ToEnumOrNullFromEnumMember<EnumTestDescription>();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetEnumValueFromDescription_Success()
        {
            // Arrange
            // Act
            var result = "Attached".GetEnumValueFromDescription<EnumTestDescription>();

            // Assert
            Assert.Equal(EnumTestDescription.Attached, result);
        }

        [Fact]
        public void GetEnumValueFromDescription_Fail()
        {
            Assert.Throws<InvalidOperationException>(() => "attached".GetEnumValueFromDescription<EnumTestDescription>());
        }

        [Fact]
        public void GetEnumValueFromDescription_IgnoreCase_Success()
        {
            // Arrange
            // Act
            var result = "attached".GetEnumValueFromDescription<EnumTestDescription>(ignoreCase: true);

            // Assert
            Assert.Equal(EnumTestDescription.Attached, result);
        }
    }
}
