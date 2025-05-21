namespace Husa.Extensions.Document.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Husa.Extensions.Document.Interfaces;
    using Husa.Extensions.Document.Models;

    public static class ChangesLogExtensions
    {
        public static void AddSectionProperties(this Dictionary<string, ChangedField> fields, Dictionary<string, (object Original, object Updated)> entityPairs, string prefix = "")
        {
            foreach (var (key, (original, updated)) in entityPairs)
            {
                if (original != null && updated != null)
                {
                    fields.AddProperties(original, updated, $"{prefix}{key}.");
                }
            }
        }

        /// <summary>
        /// Compares properties between original and updated objects and adds differences to the fields dictionary.
        /// </summary>
        /// <param name="fields">Dictionary to store the changed fields.</param>
        /// <param name="original">The original object.</param>
        /// <param name="updated">The updated object.</param>
        /// <param name="prefix">Optional prefix for field names.</param>
        public static void AddProperties(
            this Dictionary<string, ChangedField> fields,
            object original,
            object updated,
            string prefix = "",
            string[] filterFields = null,
            string[] excludeFields = null)
        {
            if (original == null || updated == null)
            {
                return;
            }

            var properties = updated.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p =>
                    p.CanRead && p.CanWrite &&
                    !p.PropertyType.FullName.StartsWith("Microsoft.EntityFrameworkCore"))
                .FilterProperties(filterFields, excludeFields);
            var summaryFields = SummaryExtensions.GetChangedFields(updated, original, properties);

            foreach (var fieldSummary in summaryFields)
            {
                string key = $"{prefix}{fieldSummary.FieldName}";
                fields[key] = new ChangedField
                {
                    OldValue = fieldSummary.OldValue,
                    NewValue = fieldSummary.NewValue,
                };
            }
        }

        public static void AddPropertiesWithComparer<T, TComparer>(
            this Dictionary<string, ChangedField> fields,
            ICollection<T> updated,
            IEnumerable<T> original,
            string fieldName,
            string[] filterFields = null,
            string[] excludeFields = null)
            where T : IProvideType
            where TComparer : IEqualityComparer<T>, new()
        {
            if (updated == null || original == null)
            {
                return;
            }

            var summaryFields = updated.GetSummaryByComparer<T, TComparer>(original, true, filterFields, excludeFields);
            if (summaryFields == null || !summaryFields.Any())
            {
                return;
            }

            fields[fieldName] = new ChangedField
            {
                OldValue = summaryFields.Select(x => x.OldValue).Where(x => x != null),
                NewValue = summaryFields.Select(x => x.NewValue).Where(x => x != null),
            };
        }
    }
}
