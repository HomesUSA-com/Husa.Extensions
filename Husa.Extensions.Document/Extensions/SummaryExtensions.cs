namespace Husa.Extensions.Document.Extensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Husa.Extensions.Common;
    using Husa.Extensions.Document.Interfaces;
    using Husa.Extensions.Document.ValueObjects;

    public static class SummaryExtensions
    {
        public static SummarySection GetSummarySection<TNewEntity, TOldEntity>(this TNewEntity newObject, TOldEntity oldObject, string sectionName, string[] filterFields = null, string[] excludeFields = null)
        {
            var summaryFields = GetFieldSummary(newObject, oldObject, filterFields: filterFields, excludeFields: excludeFields);

            if (!summaryFields.Any())
            {
                return null;
            }

            return new()
            {
                Name = sectionName,
                Fields = summaryFields,
            };
        }

        public static IEnumerable<SummaryField> GetFieldSummary<TRecord, T>(TRecord newObject, T oldObject, string[] filterFields = null, string[] excludeFields = null)
        {
            var propertiesInfo = typeof(T).GetProperties().ToList();

            if (filterFields != null && filterFields.Any())
            {
                propertiesInfo = propertiesInfo.Where(propertyInfo => filterFields.Contains(propertyInfo.Name)).ToList();
            }

            if (excludeFields != null && excludeFields.Any())
            {
                propertiesInfo = propertiesInfo.Where(propertyInfo => !excludeFields.Contains(propertyInfo.Name)).ToList();
            }

            foreach (var propertyInfo in propertiesInfo)
            {
                var newValue = propertyInfo.GetValue(newObject);
                var oldValue = oldObject is null ? null : propertyInfo.GetValue(oldObject);

                switch (newValue)
                {
                    case string newValueAsString:
                        if (!newValueAsString.Trim().EqualsTo(((string)oldValue)?.Trim()))
                        {
                            yield return new SummaryField(fieldName: propertyInfo.Name, oldValue: oldValue, newValue: newValue);
                        }

                        break;
                    case IEnumerable:
                        var underlyingType = propertyInfo.PropertyType.GetGenericArguments()[0];
                        var sequenceEqualMethod = typeof(Enumerable)
                            .GetMethods(BindingFlags.Public | BindingFlags.Static)
                            .Single(m => m.Name == nameof(Enumerable.SequenceEqual) && m.GetParameters().Length == 2)
                            .MakeGenericMethod(underlyingType);
                        if (oldValue is null && IsEmptyIEnumerable(newValue))
                        {
                            break;
                        }

                        if (oldValue is null)
                        {
                            yield return new SummaryField(propertyInfo.Name, oldValue, newValue);

                            break;
                        }

                        var (newValueOrdered, oldValueOrdered) = SortArrays(underlyingType, newValue, oldValue);
                        var valueOrdered = new object[] { newValueOrdered, oldValueOrdered };
                        var areEqual = (bool)sequenceEqualMethod.Invoke(obj: null, valueOrdered);
                        if (areEqual)
                        {
                            break;
                        }

                        yield return new SummaryField(propertyInfo.Name, oldValueOrdered, newValueOrdered);

                        break;
                    default:
                        if (object.Equals(newValue, oldValue))
                        {
                            break;
                        }

                        if (newValue is null && IsEmptyIEnumerable(oldValue))
                        {
                            break;
                        }

                        yield return new SummaryField(propertyInfo.Name, oldValue, newValue);

                        break;
                }
            }
        }

        public static IEnumerable<SummaryField> GetSummaryByComparer<T, TComparer>(this ICollection<T> currentElements, IEnumerable<T> oldElements)
            where T : IProvideType
            where TComparer : IEqualityComparer<T>, new()
        {
            static IEnumerable<SummaryField> FieldSummary(IEnumerable<T> fieldElements, bool newValues)
            {
                foreach (var field in fieldElements)
                {
                    yield return new SummaryField(field.FieldType, newValues ? null : field, newValues ? field : null);
                }
            }

            if (oldElements == null)
            {
                return FieldSummary(currentElements, newValues: true);
            }

            if (!currentElements.Any())
            {
                return FieldSummary(oldElements, newValues: false);
            }

            var summary = new List<SummaryField>();
            var equalElements = new List<T>();
            var comparer = new TComparer();

            IEnumerable<T> currentElems = currentElements;
            List<T> oldElems = oldElements.ToList();

            foreach (var currentElem in currentElems)
            {
                var oldElementIndex = oldElems.FindIndex(x => comparer.Equals(x, currentElem));
                if (oldElementIndex > -1)
                {
                    equalElements.Add(currentElem);
                    oldElems.RemoveAt(oldElementIndex);
                }
                else
                {
                    // new
                    summary.Add(new SummaryField(currentElem.FieldType, null, currentElem));
                }
            }

            if (oldElems.Any())
            {
                summary.AddRange(FieldSummary(oldElems, newValues: false));
            }

            if (summary.Any())
            {
                summary.AddRange(equalElements.Select(elem => new SummaryField(elem.FieldType, elem, elem)));
            }

            return summary;
        }

        public static void AddFieldsToSummary(List<SummarySection> summarySections, string sectionName, params SummaryField[] fieldsToAdd)
        {
            var sectionIndex = summarySections.FindIndex(x => x.Name == sectionName);
            if (sectionIndex != -1)
            {
                var section = summarySections[sectionIndex];
                section.Fields = section.Fields.Concat(fieldsToAdd);
            }
        }

        private static bool IsEmptyIEnumerable(object value)
        {
            if (value is IEnumerable enumerable)
            {
                return !enumerable.Cast<object>().Any();
            }

            return false;
        }

        private static (object NewValueOrdered, object OldValueOrdered) SortArrays(Type underlyingType, object newValue, object oldValue)
        {
            var newValueOrderParams = new[] { newValue };
            var oldValueOrderParams = new[] { oldValue };

            var orderMethod = typeof(Enumerable)
                            .GetMethods(BindingFlags.Public | BindingFlags.Static)
#if NET8_0_OR_GREATER
                            .Single(m => m.Name == nameof(Enumerable.Order) && m.GetParameters().Length == 1)
#else
                            .Single(m => m.GetParameters().Length == 1)
#endif
                            .MakeGenericMethod(underlyingType);

            var newValueOrdered = orderMethod.Invoke(obj: null, newValueOrderParams);
            var oldValueOrdered = orderMethod.Invoke(obj: null, oldValueOrderParams);

            return (newValueOrdered, oldValueOrdered);
        }
    }
}
