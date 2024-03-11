namespace Husa.Extensions.Document.Tests.Configuration
{
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using Husa.Extensions.Common.Enums;
    using Husa.Extensions.Document.Configuration;
    using Husa.Extensions.Document.Tests.Providers.Models;
    using Xunit;

    [ExcludeFromCodeCoverage]
    [Collection(nameof(ApplicationServicesFixture))]
    public class CosmosJsonDotNetSerializerTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SerializeFromStreamSuccess(bool enumAsInteger)
        {
            // Arrange
            var addressRecord = new AddressRecord
            {
                FormalAddress = "1 CACreated, Flour Bluff 75666",
                ReadableCity = "Flour Bluff",
                StreetNumber = "1",
                StreetName = "CACreated",
                State = States.Texas,
                ZipCode = "75666",
            };
            var jsonAddressInfoStream = GetJsonStream(addressRecord, enumAsInteger);

            var sut = CosmosDbBootstrapper.BuildCosmosJsonDotNetSerializer();

            // Act
            var serializedAddress = sut.FromStream<AddressRecord>(jsonAddressInfoStream);

            // Assert
            Assert.NotNull(serializedAddress);
            Assert.Equal(addressRecord, serializedAddress);
        }

        [Fact]
        public void SerializeToStreamSuccess()
        {
            // Arrange
            var addressRecord = new AddressRecord
            {
                FormalAddress = "1 CACreated, Flour Bluff 75666",
                ReadableCity = "Flour Bluff",
                StreetNumber = "1",
                StreetName = "CACreated",
                State = States.Texas,
                ZipCode = "75666",
            };

            var sut = CosmosDbBootstrapper.BuildCosmosJsonDotNetSerializer();

            // Act
            var streamAddress = sut.ToStream<AddressRecord>(addressRecord);

            // Assert
            Assert.NotNull(streamAddress);
        }

        private static Stream GetJsonStream(AddressRecord addressRecord, bool enumAsInteger = true)
        {
            var finalValue = enumAsInteger ?
                $"\"{addressRecord.State}\"" :
                $"{(int)addressRecord.State}";

            var jsonAddressInfo = $@"
            {{
                ""FormalAddress"": ""{addressRecord.FormalAddress}"",
                ""ReadableCity"": ""{addressRecord.ReadableCity}"",
                ""StreetNumber"": ""{addressRecord.StreetNumber}"",
                ""StreetName"": ""{addressRecord.StreetName}"",
                ""State"": {finalValue},
                ""ZipCode"": ""{addressRecord.ZipCode}""
            }}";

            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(jsonAddressInfo);
            writer.Flush();
            stream.Position = 0;

            return stream;
        }
    }
}
