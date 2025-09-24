namespace Husa.Extensions.Document.Specifications.Document
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Husa.Extensions.Document.Interfaces;

    public static class DocumentSpecifications
    {
        public static IQueryable<T> FilterByEntityId<T>(this IQueryable<T> query, Guid? entityId)
            where T : IDocument
        {
            return entityId.HasValue ?
                query.Where(x => x.EntityId == entityId.Value) :
                query;
        }

        public static IQueryable<T> FilterByEntityId<T>(this IQueryable<T> query, IEnumerable<Guid> entityIds)
            where T : IDocument
        {
            return entityIds?.Any() ?? false ?
                query.Where(x => entityIds.Contains(x.EntityId)) :
                query;
        }
    }
}
