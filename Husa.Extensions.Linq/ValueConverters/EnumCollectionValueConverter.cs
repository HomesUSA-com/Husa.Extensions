namespace Husa.Quicklister.Sabor.Data.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Husa.Extensions.Common;
    using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

    public class EnumCollectionValueConverter<T> : ValueConverter<ICollection<T>, string>
        where T : Enum
    {
        public EnumCollectionValueConverter()
            : base(
                  convertToProviderExpression: enumField => enumField.ToStringFromEnumMembers(),
                  convertFromProviderExpression: enumField => enumField.CsvToEnum<T>().ToList())
        {
        }
    }
}
