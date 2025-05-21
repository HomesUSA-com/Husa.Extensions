namespace Husa.Extensions.Document.Tests.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using Husa.Extensions.Document.Extensions;
    using Husa.Extensions.Document.Interfaces;
    using Husa.Extensions.Document.Models;
    using Xunit;

    public class ChangesLogExtensionsTests
    {
        [Fact]
        public void CompareProperties_WithDifferentValues_AddsChangesToDictionary()
        {
            // Arrange
            var fields = new Dictionary<string, ChangedField>();
            var original = new TestClass { Name = "Original", Value = 10 };
            var updated = new TestClass { Name = "Updated", Value = 20 };

            // Act
            fields.AddProperties(original, updated);

            // Assert
            Assert.Equal(2, fields.Count);
            Assert.Equal("Original", fields["Name"].OldValue);
            Assert.Equal("Updated", fields["Name"].NewValue);
            Assert.Equal(10, fields["Value"].OldValue);
            Assert.Equal(20, fields["Value"].NewValue);
        }

        [Fact]
        public void CompareProperties_WithSameValues_DoesNotAddChanges()
        {
            // Arrange
            var fields = new Dictionary<string, ChangedField>();
            var original = new TestClass { Name = "Same", Value = 10 };
            var updated = new TestClass { Name = "Same", Value = 10 };

            // Act
            fields.AddProperties(original, updated);

            // Assert
            Assert.Empty(fields);
        }

        [Fact]
        public void CompareProperties_WithPrefix_AddsCorrectPrefixToKeys()
        {
            // Arrange
            var fields = new Dictionary<string, ChangedField>();
            var original = new TestClass { Name = "Original" };
            var updated = new TestClass { Name = "Updated" };
            var prefix = "Test.";

            // Act
            fields.AddProperties(original, updated, prefix);

            // Assert
            Assert.Single(fields);
            Assert.True(fields.ContainsKey("Test.Name"));
            Assert.Equal("Original", fields["Test.Name"].OldValue);
            Assert.Equal("Updated", fields["Test.Name"].NewValue);
        }

        [Fact]
        public void CompareProperties_WithFilterFields_OnlyIncludesSpecifiedFields()
        {
            // Arrange
            var fields = new Dictionary<string, ChangedField>();
            var original = new TestClass { Name = "Original", Value = 10 };
            var updated = new TestClass { Name = "Updated", Value = 20 };
            var filterFields = new[] { "Name" };

            // Act
            fields.AddProperties(original, updated, filterFields: filterFields);

            // Assert
            Assert.Single(fields);
            Assert.True(fields.ContainsKey("Name"));
            Assert.False(fields.ContainsKey("Value"));
        }

        [Fact]
        public void CompareProperties_WithExcludeFields_ExcludesSpecifiedFields()
        {
            // Arrange
            var fields = new Dictionary<string, ChangedField>();
            var original = new TestClass { Name = "Original", Value = 10 };
            var updated = new TestClass { Name = "Updated", Value = 20 };
            var excludeFields = new[] { "Name" };

            // Act
            fields.AddProperties(original, updated, excludeFields: excludeFields);

            // Assert
            Assert.Single(fields);
            Assert.False(fields.ContainsKey("Name"));
            Assert.True(fields.ContainsKey("Value"));
        }

        [Fact]
        public void CompareProperties_WithNullOriginal_DoesNothing()
        {
            // Arrange
            var fields = new Dictionary<string, ChangedField>();
            TestClass original = null;
            var updated = new TestClass { Name = "Updated" };

            // Act
            fields.AddProperties(original, updated);

            // Assert
            Assert.Empty(fields);
        }

        [Fact]
        public void CompareProperties_WithNullUpdated_DoesNothing()
        {
            // Arrange
            var fields = new Dictionary<string, ChangedField>();
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
            var log = new SavedChangesLog();

            var entityPairs = new Dictionary<string, (object Original, object Updated)>
            {
                { "TestEntity", (new TestClass { Name = "Original", Value = 10 }, new TestClass { Name = "Updated", Value = 20 }) },
            };

            // Act
            log.Fields.AddSectionProperties(entityPairs);

            // Assert
            Assert.Equal(2, log.Fields.Count);
            Assert.Equal("Original", log.Fields["TestEntity.Name"].OldValue);
            Assert.Equal("Updated", log.Fields["TestEntity.Name"].NewValue);
            Assert.Equal(10, log.Fields["TestEntity.Value"].OldValue);
            Assert.Equal(20, log.Fields["TestEntity.Value"].NewValue);
        }

        [Fact]
        public void AddSectionProperties_WithPrefix_AddsCorrectPrefixToKeys()
        {
            // Arrange
            var log = new SavedChangesLog();

            var entityPairs = new Dictionary<string, (object Original, object Updated)>
            {
                { "TestEntity", (new TestClass { Name = "Original" }, new TestClass { Name = "Updated" }) },
            };
            var prefix = "Section.";

            // Act
            log.Fields.AddSectionProperties(entityPairs, prefix);

            // Assert
            Assert.Single(log.Fields);
            Assert.True(log.Fields.ContainsKey("Section.TestEntity.Name"));
            Assert.Equal("Original", log.Fields["Section.TestEntity.Name"].OldValue);
            Assert.Equal("Updated", log.Fields["Section.TestEntity.Name"].NewValue);
        }

        [Fact]
        public void AddSectionProperties_WithNullOriginal_SkipsComparison()
        {
            // Arrange
            var log = new SavedChangesLog();

            var entityPairs = new Dictionary<string, (object Original, object Updated)>
            {
                { "TestEntity", (null, new TestClass { Name = "Updated" }) },
            };

            // Act
            log.Fields.AddSectionProperties(entityPairs);

            // Assert
            Assert.Empty(log.Fields);
        }

        [Fact]
        public void AddSectionProperties_WithNullUpdated_SkipsComparison()
        {
            // Arrange
            var log = new SavedChangesLog();

            var entityPairs = new Dictionary<string, (object Original, object Updated)>
            {
                { "TestEntity", (new TestClass { Name = "Original" }, null) },
            };

            // Act
            log.Fields.AddSectionProperties(entityPairs);

            // Assert
            Assert.Empty(log.Fields);
        }

        [Fact]
        public void AddSectionProperties_WithMultipleEntities_AddsAllChanges()
        {
            // Arrange
            var log = new SavedChangesLog();

            var entityPairs = new Dictionary<string, (object Original, object Updated)>
            {
                { "Entity1", (new TestClass { Name = "Original1" }, new TestClass { Name = "Updated1" }) },
                { "Entity2", (new TestClass { Name = "Original2" }, new TestClass { Name = "Updated2" }) },
            };

            // Act
            log.Fields.AddSectionProperties(entityPairs);

            // Assert
            Assert.Equal(2, log.Fields.Count);
            Assert.Equal("Original1", log.Fields["Entity1.Name"].OldValue);
            Assert.Equal("Updated1", log.Fields["Entity1.Name"].NewValue);
            Assert.Equal("Original2", log.Fields["Entity2.Name"].OldValue);
            Assert.Equal("Updated2", log.Fields["Entity2.Name"].NewValue);
        }

        [Fact]
        public void GetPropertiesByComparer_WithDifferentRoomInfo_AddsChangesToSummary()
        {
            // Arrange
            var log = new SavedChangesLog();
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
            log.Fields.AddPropertiesWithComparer<TestRoom, TestRoomComparer>(currentRooms, oldRooms, "rooms");

            // Assert
            Assert.NotEmpty(log.Fields);
            Assert.NotNull(log.Fields["rooms"]);
        }

        [Fact]
        public void GetPropertiesByComparer_WithSameRoomInfo_DoesNotAddChanges()
        {
            // Arrange
            var log = new SavedChangesLog();
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
            log.Fields.AddPropertiesWithComparer<TestRoom, TestRoomComparer>(currentRooms, oldRooms, "rooms");

            // Assert
            Assert.Empty(log.Fields);
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
