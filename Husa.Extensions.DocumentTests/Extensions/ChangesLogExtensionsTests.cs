namespace Husa.Extensions.Document.Tests.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using Husa.Extensions.Document.Extensions;
    using Husa.Extensions.Document.Interfaces;
    using Husa.Extensions.Document.ValueObjects;
    using Xunit;

    public class ChangesLogExtensionsTests
    {
        [Fact]
        public void AddProperties_WithDifferentValues_AddsChangesToList()
        {
            // Arrange
            var fields = new List<SummaryField>();
            var original = new TestClass { Name = "Original", Value = 10 };
            var updated = new TestClass { Name = "Updated", Value = 20 };

            // Act
            fields.AddProperties(original, updated);

            // Assert
            Assert.Equal(2, fields.Count);
            var nameField = fields.Find(f => f.FieldName == "Name");
            var valueField = fields.Find(f => f.FieldName == "Value");

            Assert.NotNull(nameField);
            Assert.NotNull(valueField);
            Assert.Equal("Original", nameField.OldValue);
            Assert.Equal("Updated", nameField.NewValue);
            Assert.Equal(10, valueField.OldValue);
            Assert.Equal(20, valueField.NewValue);
        }

        [Fact]
        public void AddProperties_WithSameValues_DoesNotAddChanges()
        {
            // Arrange
            var fields = new List<SummaryField>();
            var original = new TestClass { Name = "Same", Value = 10 };
            var updated = new TestClass { Name = "Same", Value = 10 };

            // Act
            fields.AddProperties(original, updated);

            // Assert
            Assert.Empty(fields);
        }

        [Fact]
        public void AddProperties_WithPrefix_AddsCorrectPrefixToKeys()
        {
            // Arrange
            var fields = new List<SummaryField>();
            var original = new TestClass { Name = "Original" };
            var updated = new TestClass { Name = "Updated" };
            var prefix = "Test.";

            // Act
            fields.AddProperties(original, updated, prefix);

            // Assert
            Assert.Single(fields);
            var field = fields.FirstOrDefault();
            Assert.NotNull(field);
            Assert.Equal("Test.Name", field.FieldName);
            Assert.Equal("Original", field.OldValue);
            Assert.Equal("Updated", field.NewValue);
        }

        [Fact]
        public void AddProperties_WithFilterFields_OnlyIncludesSpecifiedFields()
        {
            // Arrange
            var fields = new List<SummaryField>();
            var original = new TestClass { Name = "Original", Value = 10 };
            var updated = new TestClass { Name = "Updated", Value = 20 };
            var filterFields = new[] { "Name" };

            // Act
            fields.AddProperties(original, updated, filterFields: filterFields);

            // Assert
            Assert.Single(fields);
            var field = fields.FirstOrDefault();
            Assert.NotNull(field);
            Assert.Equal("Name", field.FieldName);
            Assert.Equal("Original", field.OldValue);
            Assert.Equal("Updated", field.NewValue);
        }

        [Fact]
        public void AddProperties_WithExcludeFields_ExcludesSpecifiedFields()
        {
            // Arrange
            var fields = new List<SummaryField>();
            var original = new TestClass { Name = "Original", Value = 10 };
            var updated = new TestClass { Name = "Updated", Value = 20 };
            var excludeFields = new[] { "Name" };

            // Act
            fields.AddProperties(original, updated, excludeFields: excludeFields);

            // Assert
            Assert.Single(fields);
            var field = fields.FirstOrDefault();
            Assert.NotNull(field);
            Assert.Equal("Value", field.FieldName);
            Assert.Equal(10, field.OldValue);
            Assert.Equal(20, field.NewValue);
        }

        [Fact]
        public void AddProperties_WithNullOriginal_DoesNothing()
        {
            // Arrange
            var fields = new List<SummaryField>();
            TestClass original = null;
            var updated = new TestClass { Name = "Updated" };

            // Act
            fields.AddProperties(original, updated);

            // Assert
            Assert.Empty(fields);
        }

        [Fact]
        public void AddProperties_WithNullUpdated_DoesNothing()
        {
            // Arrange
            var fields = new List<SummaryField>();
            var original = new TestClass { Name = "Original" };
            TestClass updated = null;

            // Act
            fields.AddProperties(original, updated);

            // Assert
            Assert.Empty(fields);
        }

        [Fact]
        public void AddSectionProperties_WithDifferentValues_AddsChangesToLog()
        {
            // Arrange
            var fields = new List<SummaryField>();

            var entityPairs = new Dictionary<string, (object Original, object Updated)>
            {
                { "TestEntity", (new TestClass { Name = "Original", Value = 10 }, new TestClass { Name = "Updated", Value = 20 }) },
            };

            // Act
            fields.AddSectionProperties(entityPairs);

            // Assert
            Assert.Equal(2, fields.Count);
            var nameField = fields.Find(f => f.FieldName == "TestEntity.Name");
            var valueField = fields.Find(f => f.FieldName == "TestEntity.Value");

            Assert.NotNull(nameField);
            Assert.NotNull(valueField);
            Assert.Equal("Original", nameField.OldValue);
            Assert.Equal("Updated", nameField.NewValue);
            Assert.Equal(10, valueField.OldValue);
            Assert.Equal(20, valueField.NewValue);
        }

        [Fact]
        public void AddSectionProperties_WithPrefix_AddsCorrectPrefixToKeys()
        {
            // Arrange
            var fields = new List<SummaryField>();

            var entityPairs = new Dictionary<string, (object Original, object Updated)>
            {
                { "TestEntity", (new TestClass { Name = "Original" }, new TestClass { Name = "Updated" }) },
            };
            var prefix = "Section.";

            // Act
            fields.AddSectionProperties(entityPairs, prefix);

            // Assert
            Assert.Single(fields);
            var field = fields.FirstOrDefault();
            Assert.NotNull(field);
            Assert.Equal("Section.TestEntity.Name", field.FieldName);
            Assert.Equal("Original", field.OldValue);
            Assert.Equal("Updated", field.NewValue);
        }

        [Fact]
        public void AddSectionProperties_WithNullOriginal_SkipsComparison()
        {
            // Arrange
            var fields = new List<SummaryField>();

            var entityPairs = new Dictionary<string, (object Original, object Updated)>
            {
                { "TestEntity", (null, new TestClass { Name = "Updated" }) },
            };

            // Act
            fields.AddSectionProperties(entityPairs);

            // Assert
            Assert.Empty(fields);
        }

        [Fact]
        public void AddSectionProperties_WithNullUpdated_SkipsComparison()
        {
            // Arrange
            var fields = new List<SummaryField>();

            var entityPairs = new Dictionary<string, (object Original, object Updated)>
            {
                { "TestEntity", (new TestClass { Name = "Original" }, null) },
            };

            // Act
            fields.AddSectionProperties(entityPairs);

            // Assert
            Assert.Empty(fields);
        }

        [Fact]
        public void AddSectionProperties_WithMultipleEntities_AddsAllChanges()
        {
            // Arrange
            var fields = new List<SummaryField>();

            var entityPairs = new Dictionary<string, (object Original, object Updated)>
            {
                { "Entity1", (new TestClass { Name = "Original1" }, new TestClass { Name = "Updated1" }) },
                { "Entity2", (new TestClass { Name = "Original2" }, new TestClass { Name = "Updated2" }) },
            };

            // Act
            fields.AddSectionProperties(entityPairs);

            // Assert
            Assert.Equal(2, fields.Count);
            var field1 = fields.Find(f => f.FieldName == "Entity1.Name");
            var field2 = fields.Find(f => f.FieldName == "Entity2.Name");

            Assert.NotNull(field1);
            Assert.NotNull(field2);
            Assert.Equal("Original1", field1.OldValue);
            Assert.Equal("Updated1", field1.NewValue);
            Assert.Equal("Original2", field2.OldValue);
            Assert.Equal("Updated2", field2.NewValue);
        }

        [Fact]
        public void GetPropertiesByComparer_WithDifferentRoomInfo_AddsChangesToSummary()
        {
            // Arrange
            var fields = new List<SummaryField>();
            var currentRooms = new List<TestRoom>
            {
                new TestRoom()
                {
                    Length = 10,
                    Width = 12,
                    Features = new List<string> { "EntryPantry" },
                },
            };
            var oldRooms = new List<TestRoom>
            {
                new TestRoom()
                {
                    Length = 10,
                    Width = 12,
                    Features = new List<string> { "Pantry" },
                },
            };

            // Act
            fields.AddPropertiesWithComparer<TestRoom, TestRoomComparer>(currentRooms, oldRooms, "rooms");

            // Assert
            Assert.NotEmpty(fields);
            var roomsField = fields.Find(f => f.FieldName == "rooms");
            Assert.NotNull(roomsField);
        }

        [Fact]
        public void GetPropertiesByComparer_WithSameRoomInfo_DoesNotAddChanges()
        {
            // Arrange
            var fields = new List<SummaryField>();
            var currentRooms = new List<TestRoom>
            {
                new TestRoom()
                {
                    Length = 10,
                    Width = 12,
                    Features = new List<string> { "Pantry" },
                },
            };
            var oldRooms = new List<TestRoom>
            {
                new TestRoom()
                {
                    Length = 10,
                    Width = 12,
                    Features = new List<string> { "Pantry" },
                },
            };

            // Act
            fields.AddPropertiesWithComparer<TestRoom, TestRoomComparer>(currentRooms, oldRooms, "rooms");

            // Assert
            Assert.Empty(fields);
        }

        private class TestClass
        {
            public string Name { get; set; }
            public int Value { get; set; }
        }

        private class TestRoom : IProvideType
        {
            public int Length { get; set; }
            public int Width { get; set; }
            public ICollection<string> Features { get; set; }
            public string FieldType => "FieldType";
            public string CustomString() => this.FieldType;
        }

        private class TestRoomComparer : IEqualityComparer<TestRoom>
        {
            public bool Equals(TestRoom x, TestRoom y)
            {
                if (ReferenceEquals(x, y))
                {
                    return true;
                }

                if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                {
                    return false;
                }

                var comparerFeatures = (x.Features ?? new List<string>()).Except(y.Features ?? new List<string>());

                return x.Width == y.Width &&
                       x.Length == y.Length &&
                       !comparerFeatures.Any();
            }

            public int GetHashCode(TestRoom room)
            {
                if (ReferenceEquals(room, null))
                {
                    return 0;
                }

                int hashWidth = room.Width.GetHashCode();
                int hashLength = room.Length.GetHashCode();
                int hasFeatures = room.Features.GetHashCode();

                return hashWidth ^
                       hashLength ^
                       hasFeatures;
            }
        }
    }
}
