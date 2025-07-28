namespace Husa.Extensions.Document.ValueObjects
{
    using System;
    using System.Collections.Generic;
    using Husa.Extensions.Document.Extensions;
    using Husa.Extensions.Domain.ValueObjects;

    public class SummaryField : ValueObject
    {
        public SummaryField(string fieldName, object oldValue, object newValue, bool transformEnumToString = false)
        {
            if (string.IsNullOrWhiteSpace(fieldName))
            {
                throw new ArgumentException($"'{nameof(fieldName)}' cannot be null or whitespace.", nameof(fieldName));
            }

            this.FieldName = fieldName;

            this.OldValue = transformEnumToString ? oldValue.TransformEnumToString() : oldValue;
            this.NewValue = transformEnumToString ? newValue.TransformEnumToString() : newValue;
        }

        public SummaryField()
        {
        }

        public string FieldName { get; set; }

        public object OldValue { get; set; }

        public object NewValue { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.FieldName;
            yield return this.OldValue;
            yield return this.NewValue;
        }
    }
}
