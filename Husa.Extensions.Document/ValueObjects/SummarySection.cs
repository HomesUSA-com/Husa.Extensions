namespace Husa.Extensions.Document.ValueObjects
{
    using System.Collections.Generic;
    using Husa.Extensions.Domain.ValueObjects;

    public class SummarySection : ValueObject
    {
        public string Name { get; set; }

        public IEnumerable<SummaryField> Fields { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.Name;
            yield return this.Fields;
        }
    }
}
