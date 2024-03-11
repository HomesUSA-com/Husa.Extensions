namespace Husa.Extensions.Document.Tests.Specifications
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Husa.Extensions.Document.Interfaces;
    using Husa.Extensions.Document.Specifications;
    using Husa.Extensions.Document.Tests.Configuration;
    using Moq;
    using Xunit;

    [Collection(nameof(ApplicationServicesFixture))]
    public class BaseSpecificationsTests
    {
        [Fact]
        public void FilterById_Success()
        {
            // Arrange
            var docId = Guid.NewGuid();
            var doc1 = new Mock<IDocument>();
            doc1.Setup(x => x.Id).Returns(docId);

            var requests = new List<IDocument>
            {
                doc1.Object,
                new Mock<IDocument>().Object,
                new Mock<IDocument>().Object,
            };

            // Act
            var result = requests.AsQueryable().FilterById(docId);

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public void FilterByNonDeleted_Success()
        {
            // Arrange
            var doc1 = new Mock<IDocument>();
            doc1.Setup(x => x.IsDeleted).Returns(true);

            var requests = new List<IDocument>
            {
                doc1.Object,
                new Mock<IDocument>().Object,
                new Mock<IDocument>().Object,
            };

            // Act
            var result = requests.AsQueryable().FilterByNonDeleted();

            // Assert
            Assert.Equal(2, result.Count());
        }
    }
}
