namespace Husa.Extensions.Linq.ValueConverters
{
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

    public class StringCollectionValueConverter : ValueConverter<ICollection<string>, string>
    {
        public StringCollectionValueConverter(string separator = ",")
        : base(
                  convertToProviderExpression: elements => elements.ToStringFromCollection(separator),
                  convertFromProviderExpression: element => element.ToCollectionFromString(separator))
        {
        }
    }
}
