namespace Husa.Extensions.Tests.Validations
{
    using System.ComponentModel.DataAnnotations;
    using Husa.Extensions.Common.Validations;
    using Xunit;

    public class DataCheckerAttributeTests
    {
        [Theory]
        [InlineData("This is a safe description", true)]
        [InlineData("I am automatically out of the game", true)]
        [InlineData("This description contains www", false)]
        [InlineData("Contact us at example@domain.com", false)]
        [InlineData("No black or white only policies", false)]
        public void IsValid_ShouldValidateDescriptionCorrectly(string description, bool expectedIsValid)
        {
            // Arrange
            var attribute = new DataCheckerAttribute();
            var validationContext = new ValidationContext(new { Description = description });

            // Act
            var result = attribute.GetValidationResult(description, validationContext);

            // Assert
            if (expectedIsValid)
            {
                Assert.Equal(ValidationResult.Success, result);
            }
            else
            {
                Assert.NotEqual(ValidationResult.Success, result);
                Assert.Contains("contains forbidden words", result.ErrorMessage);
            }
        }
    }
}
