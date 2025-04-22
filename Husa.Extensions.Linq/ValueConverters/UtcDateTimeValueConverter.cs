namespace Husa.Extensions.Linq.ValueConverters
{
    using System;
    using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

    /// <summary>
    /// Converts DateTime values to UTC when saving to the database and preserves UTC when reading from the database.
    /// </summary>
    public class UtcDateTimeValueConverter : ValueConverter<DateTime, DateTime>
    {
        public UtcDateTimeValueConverter()
            : base(
                  v => v.Kind == DateTimeKind.Utc ? v : DateTime.SpecifyKind(v, DateTimeKind.Utc),
                  v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
        {
        }
    }
}
