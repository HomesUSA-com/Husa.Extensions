namespace Husa.Extensions.Document.Tests.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Husa.Extensions.Document.Interfaces;
    using Husa.Extensions.Document.Tests.Configuration;
    using Husa.Extensions.Document.Tests.Providers.Repositories;
    using Microsoft.Azure.Cosmos;
    using Moq;
    using Xunit;

    [Collection(nameof(ApplicationServicesFixture))]
    public class DocumentRepositoryTests
    {
        private readonly ApplicationServicesFixture fixture;
        private readonly Mock<Container> container = new();
        private readonly Mock<ICosmosLinqQuery> cosmosLinqQuery = new();

        public DocumentRepositoryTests(ApplicationServicesFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task GetByLegacyIdAsync_Success()
        {
            // Arrange
            var entityId = Guid.NewGuid();
            var record = new Mock<IDocument>();
            record.SetupGet(x => x.EntityId).Returns(entityId);

            var sut = this.GetSut(Array.Empty<IDocument>());

            // Act
            await sut.AddDocumentAsync(record.Object);

            // Assert
            this.container.Verify(r => r.CreateItemAsync(It.IsAny<IDocument>(), It.IsAny<PartitionKey>(), It.IsAny<ItemRequestOptions>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateDocumentAsync_Success()
        {
            // Arrange
            var documentId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var record = new Mock<IDocument>();

            var sut = this.GetSut(Array.Empty<IDocument>());

            // Act
            await sut.UpdateDocumentAsync(documentId, record.Object, userId);

            // Assert
            record.Verify(x => x.UpdateTrackValues(It.Is<Guid>(id => id == userId), It.IsAny<bool>()), Times.Once);
            this.container.Verify(x => x.ReplaceItemAsync(It.IsAny<IDocument>(), It.Is<string>(id => id == documentId.ToString()), It.IsAny<PartitionKey?>(), It.IsAny<ItemRequestOptions>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        protected void SetupGetFeedIterator<T>(IEnumerable<T> requests)
        {
            var queryableRequests = requests.AsQueryable().OrderBy(x => 0);
            var feedResponseMock = new Mock<FeedResponse<T>>();
            feedResponseMock.Setup(x => x.GetEnumerator()).Returns(queryableRequests.GetEnumerator());

            var feedIteratorMock = new Mock<FeedIterator<T>>();
            feedIteratorMock.Setup(f => f.HasMoreResults).Returns(true);
            feedIteratorMock
                .Setup(f => f.ReadNextAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(feedResponseMock.Object);

            this.cosmosLinqQuery.Setup(x => x.GetFeedIterator(It.IsAny<IQueryable<T>>())).Returns(feedIteratorMock.Object).Verifiable();
        }

        private DocumentRepository GetSut(IEnumerable<IDocument> requests)
        {
            var queryableRequests = requests.AsQueryable().OrderBy(x => 0);

            this.container.Setup(x => x.GetItemLinqQueryable<IDocument>(It.IsAny<bool>(), It.IsAny<string>(), It.IsAny<QueryRequestOptions>(), It.IsAny<CosmosLinqSerializerOptions>()))
                .Returns(queryableRequests).Verifiable();

            var client = new Mock<CosmosClient>();
            client.Setup(x => x.GetContainer(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(this.container.Object).Verifiable();

            return new DocumentRepository(client.Object, this.cosmosLinqQuery.Object, this.fixture.DocumentDbOptions.Object);
        }
    }
}
