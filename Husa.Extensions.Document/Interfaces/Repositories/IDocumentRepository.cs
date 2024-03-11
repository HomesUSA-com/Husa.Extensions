namespace Husa.Extensions.Document.Interfaces.Repositories
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IDocumentRepository<TDocument>
        where TDocument : IDocument
    {
        Task AddDocumentAsync(TDocument document, CancellationToken cancellationToken = default);

        Task UpdateDocumentAsync(Guid documentId, TDocument document, Guid userId, CancellationToken cancellationToken = default);

        Task<TDocument> GetByIdAsync(Guid documentId, CancellationToken cancellationToken = default);

        Task<TDocument> GetByLegacyIdAsync(int legacyId, CancellationToken cancellationToken = default);

        Task<TDocument> GetLastRecordAsync(Guid enityId, DateTime? limitDate = null, CancellationToken cancellationToken = default);
    }
}
