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
    public class DocumentQueriesRepositoryTests
    {
        private readonly ApplicationServicesFixture fixture;
        private readonly Mock<ICosmosLinqQuery> cosmosLinqQuery = new();

        public DocumentQueriesRepositoryTests(ApplicationServicesFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task GetLastRecordAsync_Success()
        {
            // Arrange
            var entityId = Guid.NewGuid();
            var docId = Guid.NewGuid();

            var record1 = new Mock<IDocument>();
            record1.SetupGet(x => x.EntityId).Returns(entityId);
            record1.SetupGet(x => x.SysCreatedOn).Returns(DateTime.Now.AddDays(-2));
            record1.SetupGet(x => x.Id).Returns(docId);

            var record2 = new Mock<IDocument>();
            record2.SetupGet(x => x.EntityId).Returns(entityId);
            record1.SetupGet(x => x.SysCreatedOn).Returns(DateTime.Now);

            var records = new[] { record1.Object, record2.Object };
            this.SetupGetFeedIterator(records);
            var sut = this.GetSut(records);

            // Act
            var result = await sut.GetLastRecordAsync(entityId, DateTime.Now.AddDays(-1));

            // Assert
            Assert.Equal(docId, result.Id);
        }

        [Fact]
        public async Task GetByLegacyIdAsync_Success()
        {
            // Arrange
            var legacyId = 12;
            var record = new Mock<IDocument>();
            record.SetupGet(x => x.LegacyId).Returns(legacyId);

            this.SetupGetFeedIterator(new[] { record.Object });
            var sut = this.GetSut(new[] { record.Object });

            // Act
            var result = await sut.GetByLegacyIdAsync(legacyId);

            // Assert
            Assert.NotNull(result);
        }

        private DocumentQueriesRepository GetSut(IEnumerable<IDocument> requests)
        {
            var queryableRequests = requests.AsQueryable().OrderBy(x => 0);
            var container = new Mock<Container>();

            container.Setup(x => x.GetItemLinqQueryable<IDocument>(It.IsAny<bool>(), It.IsAny<string>(), It.IsAny<QueryRequestOptions>(), It.IsAny<CosmosLinqSerializerOptions>()))
                .Returns(queryableRequests).Verifiable();

            var client = new Mock<CosmosClient>();
            client.Setup(x => x.GetContainer(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(container.Object).Verifiable();

            return new DocumentQueriesRepository(client.Object, this.cosmosLinqQuery.Object, this.fixture.DocumentDbOptions.Object);
        }

        private void SetupGetFeedIterator<T>(IEnumerable<T> requests)
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
    }
}
