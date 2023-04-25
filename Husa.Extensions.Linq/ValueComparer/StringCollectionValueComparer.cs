namespace Husa.Extensions.Linq.ValueComparer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    public class StringCollectionValueComparer : ValueComparer<ICollection<string>>
    {
        public StringCollectionValueComparer()
            : base(
                  equalsExpression: (firstCollection, secondCollection) => firstCollection.SequenceEqual(secondCollection),
                  hashCodeExpression: collection => collection.Aggregate(0, (accumulate, field) => HashCode.Combine(accumulate, field.GetHashCode())),
                  snapshotExpression: collection => collection.ToList())
        {
        }
    }
}
