namespace Husa.Extensions.Common
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
#if NET6_0
    using System.Linq.Expressions;
#endif
    using System.Reflection;
    using Husa.Extensions.Common.Interfaces;
    using Husa.Extensions.Common.ValueObjects;

    public static class SummaryExtensions
    {
        public static IEnumerable<SummaryField> GetInnerSummary<T>(T newObject, T oldObject, string[] filterFields = null)
        {
            var propertiesInfo = typeof(T).GetProperties().ToList();

            if (filterFields != null && filterFields.Any())
            {
                propertiesInfo = propertiesInfo.Where(x => !filterFields.Contains(x.Name)).ToList();
            }

            return GetSummaryFields(propertiesInfo, newObject, oldObject);
        }

        public static IEnumerable<SummaryField> GetFieldSummary<TRecord, T>(TRecord newObject, T oldObject, bool isInnerSummary = false, string[] filterFields = null)
        {
            var propertiesInfo = typeof(T).GetProperties().ToList();

            if (filterFields != null && filterFields.Any())
            {
                Func<PropertyInfo, bool> filterFunction = isInnerSummary ?
                    propertyInfo => !filterFields.Contains(propertyInfo.Name) :
                    propertyInfo => filterFields.Contains(propertyInfo.Name);

                propertiesInfo = propertiesInfo.Where(filterFunction).ToList();
            }

            foreach (var propertyInfo in propertiesInfo)
            {
                var newValue = propertyInfo.GetValue(newObject);
                var oldValue = oldObject is null ? null : propertyInfo.GetValue(oldObject);

                switch (newValue)
                {
                    case string newValueAsString:
                        if (!newValueAsString.EqualsTo((string)oldValue))
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
                        if (oldValue is null)
                        {
                            yield return new SummaryField(propertyInfo.Name, oldValue, newValue);

                            break;
                        }

                        var (newValueOrdered, oldValueOrdered) = SortArrays(underlyingType, newValue, oldValue);
                        var areEqual = (bool)sequenceEqualMethod.Invoke(obj: null, new[] { newValueOrdered, oldValueOrdered });
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

                        yield return new SummaryField(propertyInfo.Name, oldValue, newValue);

                        break;
                }
            }
        }

        public static IEnumerable<SummaryField> GetFieldSummary<T>(T newObject, T oldObject, string[] filterFields = null)
        {
            var propertiesInfo = typeof(T).GetProperties().ToList();

            if (filterFields != null && filterFields.Any())
            {
                propertiesInfo = propertiesInfo.Where(x => filterFields.Contains(x.Name)).ToList();
            }

            return GetSummaryFields(propertiesInfo, newObject, oldObject);
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

        private static IEnumerable<SummaryField> GetSummaryFields<T>(IEnumerable<PropertyInfo> properties, T newObject, T oldObject)
        {
            foreach (var propertyInfo in properties)
            {
                var newValue = propertyInfo.GetValue(newObject);
                var oldValue = oldObject is null ? null : propertyInfo.GetValue(oldObject);

                if (propertyInfo.PropertyType == typeof(bool))
                {
                    if (!newValue.Equals(oldValue))
                    {
                        yield return new SummaryField(fieldName: propertyInfo.Name, oldValue: oldValue, newValue: newValue);
                    }
                }
                else if (!object.Equals(newValue, oldValue))
                {
                    yield return new SummaryField(fieldName: propertyInfo.Name, oldValue: oldValue, newValue: newValue);
                }
            }
        }

#if NET7_0
        private static (object NewValueOrdered, object OldValueOrdered) SortArrays(Type underlyingType, object newValue, object oldValue)
        {
            var newValueOrderParams = new[] { newValue };
            var oldValueOrderParams = new[] { oldValue };

            var orderMethod = typeof(Enumerable)
                            .GetMethods(BindingFlags.Public | BindingFlags.Static)
                            .Single(m => m.Name == nameof(Enumerable.Order) && m.GetParameters().Length == 1)
                            .MakeGenericMethod(underlyingType);

            var newValueOrdered = orderMethod.Invoke(obj: null, newValueOrderParams);
            var oldValueOrdered = orderMethod.Invoke(obj: null, oldValueOrderParams);

            return (newValueOrdered, oldValueOrdered);
        }
#elif NET6_0
        private static (object NewValueOrdered, object OldValueOrdered) SortArrays(Type underlyingType, object newValue, object oldValue)
        {
            var parameter = Expression.Parameter(underlyingType, "enumOption");
            var orderByExpression = Expression.Lambda(parameter, parameter);
            var orderByFunc = orderByExpression.Compile();

            var newValueOrderParams = new[] { newValue, orderByFunc };
            var oldValueOrderParams = new[] { oldValue, orderByFunc };

            var orderMethod = typeof(Enumerable)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Single(m => m.Name == nameof(Enumerable.OrderBy) && m.GetParameters().Length == 2)
                .MakeGenericMethod(underlyingType, underlyingType);

            var newValueOrdered = orderMethod.Invoke(obj: null, newValueOrderParams);
            var oldValueOrdered = orderMethod.Invoke(obj: null, oldValueOrderParams);

            return (newValueOrdered, oldValueOrdered);
        }
#endif
    }
}
