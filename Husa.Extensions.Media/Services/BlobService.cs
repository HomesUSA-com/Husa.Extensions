namespace Husa.Extensions.Media.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using Azure.Storage.Blobs;
    using Azure.Storage.Blobs.Models;
    using Husa.Extensions.Media.Interfaces;

    public class BlobService : IBlobService
    {
        private readonly BlobContainerClient blobContainerClient;

        public BlobService(BlobContainerClient blobContainerClient)
        {
            this.blobContainerClient = blobContainerClient ?? throw new ArgumentNullException(nameof(blobContainerClient));
        }

        public async Task<Uri> AddToTemporalBlobAsync(Stream bynaryData, Guid mediaId, string contentType, IDictionary<string, string> fileMeta)
        {
            var blobName = $"temporal/{mediaId}";
            await this.blobContainerClient.UploadBlobAsync(blobName, bynaryData);
            var bblob = this.blobContainerClient.GetBlobClient(blobName);
            bblob.SetMetadata(fileMeta);
            bblob.SetHttpHeaders(new BlobHttpHeaders { ContentType = contentType });
            return bblob.Uri;
        }
    }
}
