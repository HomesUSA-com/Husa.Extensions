namespace Husa.Extensions.Downloader.Trestle.Tests
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Husa.Extensions.Downloader.Trestle.Models;
    using Husa.Extensions.Downloader.Trestle.Models.TableEntities;
    using Husa.Extensions.Downloader.Trestle.Services;
    using Microsoft.Extensions.Options;
    using Moq;
    using Xunit;

    public class TrestleClientTest
    {
        [Fact]
        public async Task GetOpenHouseWithEmptyCollectionFails()
        {
            // Arrange
            var sut = GetSut(new Mock<ITrestleRequester>());

            // Act && Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => sut.GetOpenHouse(listingsKeys: Array.Empty<string>()));
        }

        [Fact]
        public async Task GetOpenHouseWithNullCollectionFails()
        {
            // Arrange
            var sut = GetSut(new Mock<ITrestleRequester>());

            // Act && Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => sut.GetOpenHouse(listingsKeys: null));
        }

        [Fact]
        public async Task GetOpenHouseWithExistingTokenSuccess()
        {
            // Arrange
            const string openHouseClass = "OpenHouse";
            const string listingKey = "9999222";
            const string fakeToken = "fake-token-value";
            var trestleRequester = new Mock<ITrestleRequester>();

            var httpClient = new Mock<HttpClient>();
            trestleRequester
                .Setup(r => r.GetAuthenticatedClient(It.Is<string>(token => token == fakeToken)))
                .Returns(httpClient.Object);

            var sut = GetSut(trestleRequester);

            // Act
            var result = await sut.GetOpenHouse(listingsKeys: new[] { listingKey });

            // Assert
            Assert.Empty(result);
            trestleRequester.Verify(b => b.GetData<OpenHouse>(It.IsAny<HttpClient>(), It.Is<string>(dataClass => dataClass == openHouseClass), It.IsAny<string>()), Times.Once);
        }

        private static TrestleClient GetSut(Mock<ITrestleRequester> trestleRequester)
        {
            var blobTableRepository = new Mock<IBlobTableRepository>();
            var marketOptions = Options.Create(new MarketOptions());
            var blobOptions = Options.Create(new BlobOptions());

            var token = new TokenEntity
            {
                AccessToken = "fake-token-value",
                ExpireDate = DateTimeOffset.UtcNow.AddDays(1),
            };

            blobTableRepository
                .Setup(b => b.GetTokenInfoFromStorage(It.IsAny<AuthInfo>()))
                .ReturnsAsync(token);
            return new TrestleClient(trestleRequester.Object, blobTableRepository.Object, marketOptions, blobOptions);
        }
    }
}
