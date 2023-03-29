namespace Husa.Extensions.Common
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
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

                switch (newValue, oldValue)
                {
                    case (bool, bool):
                    case (string, string):
                        if (!newValue.Equals(oldValue))
                        {
                            yield return new SummaryField()
                            {
                                FieldName = propertyInfo.Name,
                                OldValue = oldValue,
                                NewValue = newValue,
                            };
                        }

                        break;
                    case (IEnumerable, IEnumerable):
                        var underlyingType = propertyInfo.PropertyType.GetGenericArguments()[0];
                        var sequenceEqualMethod = typeof(Enumerable)
                            .GetMethods(BindingFlags.Public | BindingFlags.Static)
                            .Single(m => m.Name == nameof(Enumerable.SequenceEqual) && m.GetParameters().Length == 2)
                            .MakeGenericMethod(underlyingType);

                        var orderMethod = typeof(Enumerable)
                            .GetMethods(BindingFlags.Public | BindingFlags.Static)
                            .Single(m => m.Name == nameof(Enumerable.Order) && m.GetParameters().Length == 1)
                            .MakeGenericMethod(underlyingType);

                        var newValueOrdered = orderMethod.Invoke(obj: null, new[] { newValue });
                        var oldValueOrdered = orderMethod.Invoke(obj: null, new[] { oldValue });

                        var areEqual = (bool)sequenceEqualMethod.Invoke(obj: null, new[] { newValueOrdered, oldValueOrdered });

                        if (!areEqual)
                        {
                            yield return new SummaryField()
                            {
                                FieldName = propertyInfo.Name,
                                OldValue = oldValue,
                                NewValue = newValue,
                            };
                        }

                        break;
                    default:
                        if (!object.Equals(newValue, oldValue))
                        {
                            yield return new SummaryField()
                            {
                                FieldName = propertyInfo.Name,
                                OldValue = oldValue,
                                NewValue = newValue,
                            };
                        }

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
                        yield return new SummaryField()
                        {
                            FieldName = propertyInfo.Name,
                            OldValue = oldValue,
                            NewValue = newValue,
                        };
                    }
                }
                else if (!object.Equals(newValue, oldValue))
                {
                    yield return new SummaryField()
                    {
                        FieldName = propertyInfo.Name,
                        OldValue = oldValue,
                        NewValue = newValue,
                    };
                }
            }
        }
    }
}
