namespace Husa.Extensions.Document.QueryFilters
{
    using System;

    public class RequestBaseQueryFilter
    {
        public string SearchFilter { get; set; }

        public bool IsPrint { get; set; }

        public int? Take { get; set; }

        public string SortBy { get; set; }

        public string CurrentToken { get; set; }

        public string ContinuationToken { get; set; }

        public Guid? EntityId { get; set; }
    }
}
