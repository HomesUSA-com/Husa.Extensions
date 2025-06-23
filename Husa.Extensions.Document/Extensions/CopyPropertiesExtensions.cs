namespace Husa.Extensions.Document.Extensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Husa.Extensions.Common;

    public static class CopyPropertiesExtensions
    {
        public static void CopyProperty(object entity, string fieldName, object value)
        {
            // Split the field path into parts
            var pathParts = fieldName.Split('.');
            object currentObject = entity;

            for (int i = 0; i < pathParts.Length - 1; i++)
            {
                var propertyInfo = GetProperty(currentObject, pathParts[i]);
                currentObject = GetValue(currentObject, propertyInfo);
            }

            // Get the final property that needs to be modified
            var finalPropertyName = pathParts[pathParts.Length - 1];
            var finalProperty = GetProperty(currentObject, finalPropertyName);

            // Convert the old value to the correct type if necessary
            var newValue = value != null && finalProperty.PropertyType != value.GetType()
                ? ConvertValue(finalProperty, value)
                : value;

            // Set the old value
            finalProperty.SetValue(currentObject, newValue);
        }

        private static object ConvertValue(System.Reflection.PropertyInfo propertyInfo, object value)
        {
            try
            {
                Type targetType = propertyInfo.PropertyType;

                // Handle nullable types
                Type underlyingType = Nullable.GetUnderlyingType(targetType);
                if (underlyingType != null)
                {
                    if (value == null)
                    {
                        return null;
                    }

                    targetType = underlyingType;
                }

                // Handle enumeration types
                if (targetType.IsEnum)
                {
                    return Enum.Parse(targetType, value.ToString(), ignoreCase: true);
                }
                else if (typeof(IEnumerable).IsAssignableFrom(targetType) &&
                         targetType.IsGenericType)
                {
                    return ConvertArrayOfStringToListOfEnum(propertyInfo, value);
                }
                else
                {
                    return Convert.ChangeType(value, targetType);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Could not convert value {value} to type {propertyInfo.PropertyType.Name}", ex);
            }
        }

        private static object ConvertArrayOfStringToListOfEnum(System.Reflection.PropertyInfo propertyInfo, object value)
        {
            Type elementType = propertyInfo.PropertyType.GetGenericArguments().FirstOrDefault();
            if (elementType == null || !elementType.IsEnum)
            {
                return value;
            }

            IEnumerable<string> stringValues;
            if (value is Newtonsoft.Json.Linq.JArray jArray)
            {
                // Handle JArray from Newtonsoft.Json
                stringValues = jArray.Select(item => item.ToString());
            }
            else if (value is Array oldArray)
            {
                // Handle standard .NET arrays
                stringValues = oldArray.Cast<object>().Select(o => o.ToString());
            }
            else
            {
                return value;
            }

            // Convert array to comma-separated string
            var csvString = string.Join(",", stringValues);

            var methodInfo = typeof(EnumExtensions).GetMethod("CsvToEnumerableEnum");
            var genericMethod = methodInfo.MakeGenericMethod(elementType);

            var newValue = genericMethod.Invoke(null, [csvString, false, true]);

            if (propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>))
            {
                var toListMethod = typeof(Enumerable).GetMethod("ToList")
                    .MakeGenericMethod(elementType);
                newValue = toListMethod.Invoke(null, [newValue]);
            }

            return newValue;
        }

        private static System.Reflection.PropertyInfo GetProperty(object currentObject, string property)
        => currentObject.GetType().GetProperty(property) ?? throw new InvalidOperationException($"Property {property} not found in {currentObject.GetType().Name}");

        private static object GetValue(object currentObject, System.Reflection.PropertyInfo propertyInfo)
        => propertyInfo.GetValue(currentObject) ?? throw new InvalidOperationException($"Property {propertyInfo.Name} is null");
    }
}
