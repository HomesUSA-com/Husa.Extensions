namespace Husa.Extensions.Domain.Validations
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using AutoMapper.Internal;
    using Husa.Extensions.Domain.Extensions;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class RequiredIfCollectionAttribute : ValidationAttribute
    {
        public RequiredIfCollectionAttribute(string dependentProperty, object targetValue, bool isIn)
        {
            this.TargetValue = targetValue ?? throw new ArgumentNullException(nameof(targetValue), "targetValue Cannot be null");
            this.IsIn = isIn;
            this.ErrorMessage = "value is required";
            this.DependentProperty = dependentProperty ?? throw new ArgumentNullException(nameof(dependentProperty), "dependentProperty cannot be null");
        }

        public static Func<object, object, bool, string, ValidationResult> ValidateLimitValue { get; set; }

        public static Func<object, object, object, string, ValidationResult> ValidateRange { get; set; }

        public string DependentProperty { get; set; }

        public object TargetValue { get; set; }

        public bool IsIn { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var containerType = validationContext.ObjectInstance.GetType();
            var field = containerType.GetProperty(this.DependentProperty);
            if (field != null && field.PropertyType.IsCollection())
            {
                var dependentValue = field.GetValue(validationContext.ObjectInstance, null);
                var dependentCollectionType = field.PropertyType.GetGenericArguments().Single();

                var dependentCollection = Extensions.EnumCollectionToStringCollection(dependentValue, dependentCollectionType);
                var targetValue = dependentCollectionType.IsEnum ? Extensions.EnumMemberToString(dependentCollectionType, this.TargetValue) : (string)this.TargetValue;

                var validValue = this.IsIn ? dependentCollection.Any(value => value == targetValue) : dependentCollection.All(value => value != targetValue);

                if (validValue && (value is null || (value is string && string.IsNullOrWhiteSpace((string)value))))
                {
                    return new ValidationResult(this.ErrorMessage, new[] { validationContext.MemberName });
                }
            }

            return ValidationResult.Success;
        }
    }
}
