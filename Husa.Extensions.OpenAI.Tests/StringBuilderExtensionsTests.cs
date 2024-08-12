namespace Husa.Extensions.OpenAI.Tests;

using System.Collections.Generic;
using System.Text;
using Husa.Extensions.OpenAI.Helpers;

public class StringBuilderExtensionsTests
{
    [Fact]
    public void AppendLine_ShouldAddFieldValue_WhenFieldIsNotNullOrEmpty()
    {
        // Arrange
        var stringBuilder = new StringBuilder();
        var myObject = new { Property = "Value" };

        // Act
        stringBuilder.AppendLine(obj: myObject, value: "Value", fieldName: "Property");

        // Assert
        var result = stringBuilder.ToString();
        Assert.Contains("- Property: Value", result);
    }

    [Fact]
    public void AppendLine_ShouldNotAddFieldValue_WhenFieldIsNull()
    {
        // Arrange
        var stringBuilder = new StringBuilder();
        var myObject = new { Property = (string)null };

        // Act
        stringBuilder.AppendLine(obj: myObject, value: null, fieldName: "Property");

        // Assert
        var result = stringBuilder.ToString();
        Assert.DoesNotContain("- Property: ", result);
    }

    [Fact]
    public void AppendLine_ShouldHandleIEnumerableFieldValues()
    {
        // Arrange
        var stringBuilder = new StringBuilder();
        var myObject = new { Property = new List<string> { "Value1", "Value2" } };

        // Act
        stringBuilder.AppendLine(obj: myObject, value: myObject.Property, fieldName: "Property");

        // Assert
        var result = stringBuilder.ToString();
        Assert.Contains("- Property: Value1, Value2", result);
    }
}
