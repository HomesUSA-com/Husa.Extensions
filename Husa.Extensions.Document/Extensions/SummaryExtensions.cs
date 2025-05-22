namespace Husa.Extensions.Document.Extensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
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
            var propertiesInfo = typeof(T).GetProperties().FilterProperties(filterFields, excludeFields);
            return GetChangedFields(newObject, oldObject, propertiesInfo);
        }

        public static IEnumerable<PropertyInfo> FilterProperties(this IEnumerable<PropertyInfo> propertiesInfo, string[] filterFields = null, string[] excludeFields = null)
        {
            if (filterFields != null && filterFields.Length != 0)
            {
                propertiesInfo = propertiesInfo.Where(propertyInfo => filterFields.Contains(propertyInfo.Name)).ToList();
            }

            if (excludeFields != null && excludeFields.Length != 0)
            {
                propertiesInfo = propertiesInfo.Where(propertyInfo => !excludeFields.Contains(propertyInfo.Name)).ToList();
            }

            return propertiesInfo;
        }

        public static IEnumerable<SummaryField> GetChangedFields<TRecord, T>(TRecord newObject, T oldObject, IEnumerable<PropertyInfo> propertiesInfo)
        {
            foreach (var propertyInfo in propertiesInfo)
            {
                var newValue = propertyInfo.GetValue(newObject);
                var oldValue = oldObject is null ? null : propertyInfo.GetValue(oldObject);

                switch (newValue)
                {
                    case string newValueAsString:
                        var oldString = ((string)oldValue ?? string.Empty).Trim();
                        var newString = newValueAsString.Trim();
                        if (!string.Equals(oldString, newString, StringComparison.Ordinal))
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

        public static IEnumerable<SummaryField> GetSummaryByComparer<T, TComparer>(
            this ICollection<T> currentElements,
            IEnumerable<T> oldElements,
            bool ignoreEqualElements = false,
            string[] filterFields = null,
            string[] excludeFields = null)
            where T : IProvideType
            where TComparer : IEqualityComparer<T>, new()
        {
            if (oldElements == null)
            {
                return currentElements.GetSummaryFields(true, filterFields, excludeFields);
            }

            if (!currentElements.Any())
            {
                return oldElements.GetSummaryFields(false, filterFields, excludeFields);
            }

            var summary = new List<SummaryField>();
            var equalElements = new List<SummaryField>();
            var comparer = new TComparer();
            var oldElems = oldElements.ToList();

            foreach (var currentElem in currentElements)
            {
                var oldElementIndex = oldElems.FindIndex(x => comparer.Equals(x, currentElem));
                if (oldElementIndex > -1)
                {
                    if (!ignoreEqualElements)
                    {
                        var newElem = FilterObjectProperties(currentElem, filterFields, excludeFields);
                        equalElements.Add(new SummaryField(currentElem.FieldType, newElem, newElem));
                    }

                    oldElems.RemoveAt(oldElementIndex);
                }
                else
                {
                    summary.Add(new SummaryField(currentElem.FieldType, null, FilterObjectProperties(currentElem, filterFields, excludeFields)));
                }
            }

            if (oldElems.Count != 0)
            {
                summary.AddRange(oldElems.GetSummaryFields(false, filterFields, excludeFields));
            }

            if (summary.Count != 0)
            {
                summary.AddRange(equalElements);
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

        private static object FilterObjectProperties<T>(
            T obj,
            string[] filterFields = null,
            string[] excludeFields = null)
        {
            if (filterFields == null && excludeFields == null)
            {
                return obj;
            }

            var properties = obj.GetType().GetProperties().FilterProperties(filterFields, excludeFields);
            return properties.ToDictionary(p => p.Name, p => p.GetValue(obj));
        }

        private static IEnumerable<SummaryField> GetSummaryFields<T>(
            this IEnumerable<T> fieldElements,
            bool newValues,
            string[] filterFields = null,
            string[] excludeFields = null)
            where T : IProvideType
        {
            foreach (var field in fieldElements)
            {
                var fieldObj = FilterObjectProperties(field, filterFields, excludeFields);
                yield return new SummaryField(field.FieldType, newValues ? null : fieldObj, newValues ? fieldObj : null);
            }
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
