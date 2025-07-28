namespace Husa.Extensions.Document.Tests.Extensions
{
    using System;
    using System.Collections.Generic;
    using Husa.Extensions.Document.Extensions;
    using Husa.Extensions.Document.Models;
    using Husa.Extensions.Document.ValueObjects;
    using Xunit;

    public class CopyPropertiesExtensionsTests
    {
        private enum TestStatus
        {
            Inactive,
            Active,
            Pending,
            Deleted,
        }

        [Fact]
        public void UndoChanges_WithSimpleProperty_UpdatesEntityCorrectly()
        {
            // Arrange
            var entity = new TestClass { Name = "Current", Value = 30 };
            var document = new SavedChangesLog
            {
                Fields = new List<SummaryField>
                {
                    new() { FieldName = "Name", OldValue = "Original", NewValue = "Updated" },
                    new() { FieldName = "Value", OldValue = 10, NewValue = 20 },
                },
            };

            // Act
            UndoChanges(entity, document);

            // Assert
            Assert.Equal("Original", entity.Name);
            Assert.Equal(10, entity.Value);
        }

        [Fact]
        public void UndoChanges_WithNestedProperty_UpdatesEntityCorrectly()
        {
            // Arrange
            var entity = new TestParent
            {
                Child = new TestClass { Name = "Current", Value = 30 },
            };
            var field = new SummaryField() { FieldName = "Child.Name", OldValue = "Original", NewValue = "Updated" };

            // Act
            CopyPropertiesExtensions.CopyProperty(entity, field.FieldName, field.OldValue);

            // Assert
            Assert.Equal("Original", entity.Child.Name);
            Assert.Equal(30, entity.Child.Value); // Unchanged
        }

        [Theory]
        [InlineData("Inactive")]
        [InlineData("inactive")]
        public void UndoChanges_WithEnumProperty_ConvertsEnumCorrectly(string oldValue)
        {
            // Arrange
            var entity = new TestEntityWithEnum { Status = TestStatus.Active };
            var field = new SummaryField() { FieldName = "Status", OldValue = oldValue, NewValue = "Active" };

            // Act
            CopyPropertiesExtensions.CopyProperty(entity, field.FieldName, field.OldValue);

            // Assert
            Assert.Equal(TestStatus.Inactive, entity.Status);
        }

        [Fact]
        public void UndoChanges_WithEnumCollection_ConvertsCollectionCorrectly()
        {
            // Arrange
            var entity = new TestEntityWithEnumCollection
            {
                Statuses = new List<TestStatus> { TestStatus.Active, TestStatus.Pending },
            };
            var field = new SummaryField()
            {
                FieldName = "Statuses",
                OldValue = new[] { "Inactive", "Deleted" },
                NewValue = new[] { "Active", "Pending" },
            };

            // Act
            CopyPropertiesExtensions.CopyProperty(entity, field.FieldName, field.OldValue);

            // Assert
            Assert.Collection(
                entity.Statuses,
                item => Assert.Equal(TestStatus.Inactive, item),
                item => Assert.Equal(TestStatus.Deleted, item));
        }

        [Fact]
        public void UndoChanges_WithInvalidProperty_ThrowsInvalidOperationException()
        {
            // Arrange
            var entity = new TestClass { Name = "Current", Value = 30 };
            var field = new SummaryField() { FieldName = "NonExistentProperty", OldValue = "Original", NewValue = "Updated" };

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() =>
                CopyPropertiesExtensions.CopyProperty(entity, field.FieldName, field.OldValue));
            Assert.Contains("not found", exception.Message);
        }

        [Fact]
        public void UndoChanges_WithInvalidTypeConversion_ThrowsInvalidOperationException()
        {
            // Arrange
            var entity = new TestClass { Name = "Current", Value = 30 };
            var field = new SummaryField() { FieldName = "Value", OldValue = "NotANumber", NewValue = 20 };

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() =>
                CopyPropertiesExtensions.CopyProperty(entity, field.FieldName, field.OldValue));
            Assert.Contains("Could not convert value", exception.Message);
        }

        private static void UndoChanges(object entity, SavedChangesLog document)
        {
            foreach (var field in document.Fields)
            {
                CopyPropertiesExtensions.CopyProperty(entity, field.FieldName, field.OldValue);
            }
        }

        // Additional test classes needed for the tests
        private class TestClass
        {
            public string Name { get; set; }
            public int Value { get; set; }
        }

        private class TestParent
        {
            public TestClass Child { get; set; }
        }

        private class TestEntityWithEnum
        {
            public TestStatus Status { get; set; }
        }

        private class TestEntityWithEnumCollection
        {
            public ICollection<TestStatus> Statuses { get; set; }
        }
    }
}
