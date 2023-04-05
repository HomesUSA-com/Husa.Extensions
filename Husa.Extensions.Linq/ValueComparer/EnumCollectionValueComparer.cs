namespace Husa.Extensions.Linq.ValueComparer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    public class EnumCollectionValueComparer<T> : ValueComparer<ICollection<T>>
        where T : Enum
    {
        public EnumCollectionValueComparer()
            : base(
                  equalsExpression: (firstCollection, secondCollection) => firstCollection.SequenceEqual(secondCollection),
                  hashCodeExpression: collection => collection.Aggregate(0, (accumulate, enumField) => HashCode.Combine(accumulate, enumField.GetHashCode())),
                  snapshotExpression: collection => collection.ToList())
        {
        }
    }
}
