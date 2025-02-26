namespace Husa.Extensions.Document.Tests.Configuration
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Husa.Extensions.Document.Extensions;
    using Xunit;

    [ExcludeFromCodeCoverage]
    [Collection(nameof(ApplicationServicesFixture))]
    public class RequestSummaryTests
    {
        public class SummaryExtensionsTests
        {
            [Theory]
            [InlineData(null, null, 0)]
            [InlineData(null, "", 0)]
            [InlineData("", null, 0)]
            [InlineData("", "   ", 0)]
            [InlineData("  ", "", 0)]
            [InlineData("abc", "abc", 0)]
            [InlineData("ABC", "abc", 1)]
            [InlineData("abc", "def", 1)]
            [InlineData("Hello ", "Hello", 0)]
            public void GetFieldSummary_StringComparisons(
                string newVal,
                string oldVal,
                int expectedChanges)
            {
                // Arrange
                var newObj = new DummyEntity { FieldA = newVal };
                var oldObj = new DummyEntity { FieldA = oldVal };

                // Act
                var result = SummaryExtensions.GetFieldSummary(newObj, oldObj);

                var count = result.Count();

                Assert.Equal(expectedChanges, count);
                if (expectedChanges > 0)
                {
                    var firstChange = result.First();
                    Assert.Equal("FieldA", firstChange.FieldName);
                    Assert.Equal(oldVal, firstChange.OldValue);
                    Assert.Equal(newVal, firstChange.NewValue);
                }
            }
        }
    }
}
