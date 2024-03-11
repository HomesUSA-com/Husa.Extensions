namespace Husa.Extensions.Document.Specifications
{
    using System;
    using System.Linq;
    using Husa.Extensions.Document.Interfaces;

    public static class BaseSpecifications
    {
        public static IQueryable<T> FilterById<T>(this IQueryable<T> records, Guid id)
            where T : IDocument
        {
            return records.Where(request => request.Id == id);
        }

        public static IQueryable<T> FilterByNonDeleted<T>(this IQueryable<T> records)
            where T : IDocument
        {
            return records.Where(x => !x.IsDeleted);
        }
    }
}
