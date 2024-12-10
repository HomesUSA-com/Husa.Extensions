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
            fileMeta.TryGetValue("Extension", out var ext);
            var blobName = string.IsNullOrEmpty(ext) ? $"temporal/{mediaId}" : $"temporal/{mediaId}.{ext}";
            await this.blobContainerClient.UploadBlobAsync(blobName, bynaryData);
            var bblob = this.blobContainerClient.GetBlobClient(blobName);
            bblob.SetMetadata(fileMeta);
            bblob.SetHttpHeaders(new BlobHttpHeaders { ContentType = contentType });
            return bblob.Uri;
        }

        public async Task<Uri> UploadToBlob(IFormFile file, int width, int height)
        {
            if (!file.IsValid())
            {
                throw new ArgumentException($"Invalid file {file.FileName}", nameof(file));
            }

            var resizeImage = file.ContentType.Contains("image") && width > 0 && height > 0;
            using var memoryStream = resizeImage ? file.ResizeAndOpenStream(width, height) : file.OpenReadStream();
            memoryStream.Position = 0;

            return await this.GetBlobUri(memoryStream, file.ToDict());
        }

        public async Task<Uri> UploadToBlob(IFormFile file, Dictionary<string, string> metadata)
        {
            if (!file.IsValid())
            {
                throw new ArgumentException($"Invalid file {file.FileName}", nameof(file));
            }

            var fileMeta = file.ToDict(metadata);
            using var memoryStream = file.OpenReadStream();
            memoryStream.Position = 0;

            return await this.GetBlobUri(memoryStream, fileMeta);
        }

        private async Task<Uri> GetBlobUri(Stream file, Dictionary<string, string> metadata)
        {
            metadata.TryGetValue("ContentType", out string contentType);
            return await this.AddToTemporalBlobAsync(file, Guid.NewGuid(), contentType, metadata);
        }
    }
}
