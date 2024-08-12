namespace Husa.Extensions.OpenAI.Helpers;

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

public static class StringBuilderExtensions
{
    public static StringBuilder AppendLine(this StringBuilder stringBuilder, object obj, object value, string fieldName)
    {
        if (value == null || string.IsNullOrWhiteSpace(fieldName))
        {
            return stringBuilder;
        }

        var fieldValue = value.ToString() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(fieldValue))
        {
            return stringBuilder;
        }

        var fieldDescription = GetPropertyDescriptionNew(obj, fieldName) ?? fieldName;
        if (value is IEnumerable<string> enumerable)
        {
            var valuesArray = enumerable.ToArray();
            if (valuesArray.Length == 0)
            {
                return stringBuilder;
            }

            fieldValue = string.Join(", ", enumerable);
        }

        return stringBuilder.AppendLine($"- {fieldDescription}: {fieldValue}");
    }

    private static string GetPropertyDescriptionNew(object obj, string propertyName)
    {
        if (string.IsNullOrEmpty(propertyName))
        {
            return null;
        }

        var type = obj.GetType();
        var property = type.GetProperty(propertyName);

        if (property == null)
        {
            return null;
        }

        var descriptionAttribute = property.GetCustomAttribute<DescriptionAttribute>();
        return descriptionAttribute?.Description;
    }
}
