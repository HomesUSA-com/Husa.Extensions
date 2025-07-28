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

        public static IEnumerable<SummaryField> GetFieldSummary<TRecord, T>(TRecord newObject, T oldObject, string[] filterFields = null, string[] excludeFields = null, string namePrefix = "")
        {
            var propertiesInfo = typeof(T).GetProperties().FilterProperties(filterFields, excludeFields);
            foreach (var propertyInfo in propertiesInfo)
            {
                var newValue = propertyInfo.GetValue(newObject);
                var oldValue = oldObject is null ? null : propertyInfo.GetValue(oldObject);

                var summaryField = propertyInfo.GetCustomFieldChanges(newValue, oldValue, namePrefix: namePrefix, transformEnumToString: false);
                if (summaryField != null)
                {
                    yield return summaryField;
                }
            }
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

        public static SummaryField GetCustomFieldChanges(this PropertyInfo propertyInfo, object newValue, object oldValue, string namePrefix, bool transformEnumToString = false)
        {
            var fieldName = string.IsNullOrWhiteSpace(namePrefix) ? propertyInfo.Name : $"{namePrefix}{propertyInfo.Name}";
            switch (newValue)
            {
                case string newValueAsString:
                    var oldString = ((string)oldValue ?? string.Empty).Trim();
                    var newString = newValueAsString.Trim();
                    if (!string.Equals(oldString, newString, StringComparison.Ordinal))
                    {
                        return new SummaryField(fieldName, oldValue: oldValue, newValue: newValue, transformEnumToString);
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
                        return null;
                    }

                    if (oldValue is null)
                    {
                        return new SummaryField(fieldName, oldValue: oldValue, newValue: newValue, transformEnumToString);
                    }

                    var (newValueOrdered, oldValueOrdered) = SortArrays(underlyingType, newValue, oldValue);
                    var valueOrdered = new object[] { newValueOrdered, oldValueOrdered };
                    var areEqual = (bool)sequenceEqualMethod.Invoke(obj: null, valueOrdered);
                    if (areEqual)
                    {
                        return null;
                    }

                    return new SummaryField(fieldName, oldValue: oldValueOrdered, newValue: newValueOrdered, transformEnumToString);
                default:
                    if (object.Equals(newValue, oldValue) || (newValue is null && IsEmptyIEnumerable(oldValue)))
                    {
                        return null;
                    }

                    return new SummaryField(fieldName, oldValue: oldValue, newValue: newValue, transformEnumToString);
            }

            return null;
        }

        public static IEnumerable<SummaryField> GetSummaryByComparer<T, TComparer>(
            this ICollection<T> currentElements,
            IEnumerable<T> oldElements,
            bool ignoreEqualElements = false,
            string[] filterFields = null,
            string[] excludeFields = null,
            bool transformEnumToString = false)
            where T : IProvideType
            where TComparer : IEqualityComparer<T>, new()
        {
            if (oldElements == null)
            {
                return GetSummaryFields(currentElements, true, filterFields, excludeFields, transformEnumToString);
            }

            if (!currentElements.Any())
            {
                return GetSummaryFields(oldElements, false, filterFields, excludeFields, transformEnumToString);
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
                        equalElements.Add(new SummaryField(currentElem.FieldType, newElem, newElem, transformEnumToString));
                    }

                    oldElems.RemoveAt(oldElementIndex);
                }
                else
                {
                    var newElement = FilterObjectProperties(currentElem, filterFields, excludeFields);
                    summary.Add(new SummaryField(currentElem.FieldType, null, newElement, transformEnumToString));
                }
            }

            if (oldElems.Count != 0)
            {
                summary.AddRange(GetSummaryFields(oldElems, false, filterFields, excludeFields, transformEnumToString));
            }

            if (summary.Count != 0)
            {
                summary.AddRange(equalElements);
            }

            return summary;

            static IEnumerable<SummaryField> GetSummaryFields(
                IEnumerable<T> fieldElements,
                bool newValues,
                string[] filterFields,
                string[] excludeFields,
                bool transformEnumToString)
            {
                foreach (var field in fieldElements)
                {
                    var fieldObj = FilterObjectProperties(field, filterFields, excludeFields);
                    yield return new SummaryField(field.FieldType, newValues ? null : fieldObj, newValues ? fieldObj : null, transformEnumToString);
                }
            }
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
