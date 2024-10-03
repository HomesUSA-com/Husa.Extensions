namespace Husa.Extensions.Linq.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Husa.Extensions.Common;
    using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

    public class EnumStringCollectionValueConverter<T> : ValueConverter<ICollection<T>, string>
        where T : Enum
    {
        public EnumStringCollectionValueConverter()
            : base(
                  convertToProviderExpression: enumField => enumField.ToStringFromEnumMembers(false),
                  convertFromProviderExpression: enumField => enumField.CsvToEnum<T>(false).ToList())
        {
        }
    }
}
