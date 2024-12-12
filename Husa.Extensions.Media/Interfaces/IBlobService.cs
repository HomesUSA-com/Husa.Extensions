namespace Husa.Extensions.Media.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    public interface IBlobService
    {
        Task<Uri> AddToTemporalBlobAsync(Stream bynaryData, Guid mediaId, string contentType, IDictionary<string, string> fileMeta);

        Task<Uri> UploadToBlob(IFormFile file, int width, int height);
    }
}
