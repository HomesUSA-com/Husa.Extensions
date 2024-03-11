namespace Husa.Extensions.Document.Tests.Configuration
{
    using Microsoft.Extensions.Options;
    using Moq;

    public class ApplicationServicesFixture
    {
        public ApplicationServicesFixture()
        {
            this.DocumentDbOptions = new Mock<IOptions<DocumentDbSettings>>();
            this.DocumentDbOptions.Setup(o => o.Value).Returns(new DocumentDbSettings
            {
                Endpoint = "Endpoint",
                AuthToken = "AuthToken",
                DatabaseName = "DatabaseName",
                CollectionName = "saleRequest",
            });
        }

        public Mock<IOptions<DocumentDbSettings>> DocumentDbOptions { get; set; }
    }
}
