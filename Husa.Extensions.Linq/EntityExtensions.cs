namespace Husa.Extensions.Linq
{
    using System;
    using Husa.Extensions.Linq.ValueComparer;
    using Husa.Extensions.Linq.ValueConverters;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public static class EntityExtensions
    {
        public static PropertyBuilder HasEnumCollectionValue<TEnum>(
         this PropertyBuilder propertyBuilder,
         int maxLength,
         bool isRequired = false)
           where TEnum : Enum
        {
            propertyBuilder
                .HasMaxLength(maxLength)
                .IsRequired(isRequired)
                .HasConversion<EnumCollectionValueConverter<TEnum>>(valueComparer: new EnumCollectionValueComparer<TEnum>());
            return propertyBuilder;
        }

        public static PropertyBuilder HasEnumFieldValue<TEnum>(
         this PropertyBuilder propertyBuilder,
         int maxLength,
         bool isRequired = false)
           where TEnum : Enum
        {
            propertyBuilder
                .HasMaxLength(maxLength)
                .IsRequired(isRequired)
                .HasConversion<EnumFieldValueConverter<TEnum>>();
            return propertyBuilder;
        }
    }
}
