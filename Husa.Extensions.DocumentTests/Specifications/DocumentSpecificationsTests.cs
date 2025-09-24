namespace Husa.Extensions.Document.Tests.Specifications
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Husa.Extensions.Document.Interfaces;
    using Husa.Extensions.Document.Specifications.Document;
    using Husa.Extensions.Document.Tests.Configuration;
    using Moq;
    using Xunit;

    [Collection(nameof(ApplicationServicesFixture))]
    public class DocumentSpecificationsTests
    {
        [Fact]
        public void FilterByEntityId_Success()
        {
            // Arrange
            var entityId = Guid.NewGuid();
            var doc1 = new Mock<IDocument>();
            doc1.Setup(x => x.EntityId).Returns(entityId);

            var requests = new List<IDocument>
            {
                doc1.Object,
                new Mock<IDocument>().Object,
                new Mock<IDocument>().Object,
            };

            // Act
            var result = requests.AsQueryable().FilterByEntityId(entityId);

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public void FilterByEntityId_WithMatchingEntityIds_FiltersCorrectly()
        {
            // Arrange
            var entityId1 = Guid.NewGuid();
            var entityId2 = Guid.NewGuid();
            var doc1 = new Mock<IDocument>();
            doc1.SetupGet(x => x.EntityId).Returns(entityId1);
            var doc2 = new Mock<IDocument>();
            doc2.SetupGet(x => x.EntityId).Returns(entityId2);
            var doc3 = new Mock<IDocument>();
            doc3.SetupGet(x => x.EntityId).Returns(Guid.NewGuid());

            var documents = new List<IDocument> { doc1.Object, doc2.Object, doc3.Object };
            var filterIds = new[] { entityId1, entityId2 };

            // Act
            var result = documents.AsQueryable().FilterByEntityId(filterIds);

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, d => d.EntityId == entityId1);
            Assert.Contains(result, d => d.EntityId == entityId2);
        }

        [Fact]
        public void FilterByEntityId_WithEmptyEntityIds_ReturnsAll()
        {
            // Arrange
            var doc1 = new Mock<IDocument>();
            doc1.SetupGet(x => x.EntityId).Returns(Guid.NewGuid());
            var doc2 = new Mock<IDocument>();
            doc2.SetupGet(x => x.EntityId).Returns(Guid.NewGuid());

            var documents = new List<IDocument> { doc1.Object, doc2.Object };
            var filterIds = Array.Empty<Guid>();

            // Act
            var result = documents.AsQueryable().FilterByEntityId(filterIds);

            // Assert
            Assert.Equal(documents.Count, result.Count());
        }

        [Fact]
        public void FilterByEntityId_WithNullEntityIds_ReturnsAll()
        {
            // Arrange
            var doc1 = new Mock<IDocument>();
            doc1.SetupGet(x => x.EntityId).Returns(Guid.NewGuid());
            var doc2 = new Mock<IDocument>();
            doc2.SetupGet(x => x.EntityId).Returns(Guid.NewGuid());

            var documents = new List<IDocument> { doc1.Object, doc2.Object };
            var entityIds = (Guid[])null;

            // Act
            var result = documents.AsQueryable().FilterByEntityId(entityIds);

            // Assert
            Assert.Equal(documents.Count, result.Count());
        }
    }
}
