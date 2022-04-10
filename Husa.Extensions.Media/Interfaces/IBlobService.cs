namespace Husa.Extensions.Media.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    public interface IBlobService
    {
        Task<Uri> AddToTemporalBlobAsync(Stream bynaryData, Guid mediaId, string contentType, IDictionary<string, string> fileMeta);
    }
}
