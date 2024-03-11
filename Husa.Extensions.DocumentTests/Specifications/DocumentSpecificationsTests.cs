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
    }
}
