namespace Husa.Extensions.Common.ValueObjects
{
    using System.Collections.Generic;

    public class SummaryField : ValueObject
    {
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
