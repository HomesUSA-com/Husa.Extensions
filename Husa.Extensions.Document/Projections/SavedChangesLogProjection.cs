namespace Husa.Extensions.Document.Projections
{
    using System;
    using System.Linq.Expressions;
    using Husa.Extensions.Document.Models;

    public static class SavedChangesLogProjection
    {
        public static Expression<Func<SavedChangesLog, SavedChangesLogQueryResult>> ToProjection => saleProperty
            => new()
            {
                Id = saleProperty.Id,
                EntityId = saleProperty.EntityId,
                UserId = saleProperty.UserId,
                UserName = saleProperty.UserName,
                SavedAt = saleProperty.SavedAt,
                Fields = saleProperty.Fields,
            };
    }
}
