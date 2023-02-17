namespace Husa.Quicklister.Sabor.Data.ValueComparer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    public class StringCollectionValueComparer<T> : ValueComparer<ICollection<T>>
        where T : Enum
    {
        public StringCollectionValueComparer()
            : base(
                  equalsExpression: (firstCollection, secondCollection) => firstCollection.SequenceEqual(secondCollection),
                  hashCodeExpression: collection => collection.Aggregate(0, (accumulate, enumField) => HashCode.Combine(accumulate, enumField.GetHashCode())),
                  snapshotExpression: collection => collection.ToList())
        {
        }
    }
}
