namespace Husa.Extensions.Linq.ValueConverters
{
    using System;
    using Husa.Extensions.Common;
    using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

    public class EnumFieldValueConverter<T> : ValueConverter<T, string>
        where T : Enum
    {
        public EnumFieldValueConverter()
            : base(
                  convertToProviderExpression: enumField => enumField.ToStringFromEnumMember(),
                  convertFromProviderExpression: enumField => enumField.ToEnumFromEnumMember<T>())
        {
        }
    }
}
