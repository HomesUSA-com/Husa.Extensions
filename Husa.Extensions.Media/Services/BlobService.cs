namespace Husa.Extensions.Media.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using Azure.Storage.Blobs;
    using Azure.Storage.Blobs.Models;
    using Husa.Extensions.Media.Extensions;
    using Husa.Extensions.Media.Interfaces;
    using Microsoft.AspNetCore.Http;

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

        public Task<Uri> UploadToBlob(IFormFile file, int width, int height)
        {
            if (!file.IsValid())
            {
                throw new ArgumentException($"Invalid file {file.FileName}", nameof(file));
            }

            return this.GetBlobUri(file, width, height);
        }

        private async Task<Uri> GetBlobUri(IFormFile file, int width, int height)
        {
            var resourceId = Guid.NewGuid();

            // Set Image MetaData
            var fileMeta = new Dictionary<string, string>
            {
                { "FileName", file.FileName },
                { "FileContentType", file.ContentType },
            };

            var resizeImage = file.ContentType.Contains("image") && width != 0 && height != 0;
            using var memoryStream = resizeImage ? file.ResizeAndOpenStream(width, height) : file.OpenReadStream();
            memoryStream.Position = 0;
            var uri = await this.AddToTemporalBlobAsync(memoryStream, resourceId, file.ContentType, fileMeta);
            return uri;
        }
    }
}
