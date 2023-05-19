namespace Husa.Extensions.Common.Tests
{
    using Husa.Extensions.Common.Enums;
    using Xunit;

    public class ComparerTests
    {
        [Fact]
        public void CompareValuesReturnTrue()
        {
            // Arrange
            var dependentValue = "test";
            var targetValue = "test2";

            // Act
            var result = Comparer.CompareValues(dependentValue, targetValue, OperatorType.NotEqual, ComparedValueType.String);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CompareValuesReturnFalse()
        {
            // Arrange
            var dependentValue = "test";
            var targetValue = "test";

            // Act
            var result = Comparer.CompareValues(dependentValue, targetValue, OperatorType.Equal, ComparedValueType.String);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void TypeOfValueReturnNull()
        {
            // Arrange
            var value = string.Empty;

            // Act
            var result = Comparer.TypeOfValue(value);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void TypeOfValueReturnDateTimeValue()
        {
            // Arrange
            var value = "2022-08-10T17:28:43.97";

            // Act
            var result = Comparer.TypeOfValue(value);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ComparedValueType>(result);
            Assert.Equal(ComparedValueType.DateTime.ToString(), result.ToString());
        }

        [Fact]
        public void TypeOfValueReturnNumericValue()
        {
            // Arrange
            var value = "3,2";

            // Act
            var result = Comparer.TypeOfValue(value);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ComparedValueType>(result);
            Assert.Equal(ComparedValueType.NumericArray.ToString(), result.ToString());
        }
    }
}
