namespace Husa.Extensions.Common.Validations
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Husa.Extensions.Common.Enums;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class IfRequiredAttribute : ValidationAttribute
    {
        public IfRequiredAttribute()
        {
            this.MinRange = null;
            this.MaxRange = null;
        }

        public IfRequiredAttribute(
            string dependentProperty,
            object targetValue,
            OperatorType operatorType,
            object minRange,
            object maxRange,
            Func<object, object, object, string, ValidationResult> validateRange)
            : this(dependentProperty, targetValue, operatorType)
        {
            this.MinRange = minRange;
            this.MaxRange = maxRange;
            ValidateRange = validateRange;
        }

        public IfRequiredAttribute(
            string dependentProperty,
            object targetValue,
            OperatorType operatorType,
            object minRange,
            bool maxLimit,
            Func<object, object, bool, string, ValidationResult> validateLimit)
            : this(dependentProperty, targetValue, operatorType)
        {
            ValidateLimitValue = validateLimit;
            this.MinRange = minRange;
            this.MaxRange = maxLimit;
        }

        public IfRequiredAttribute(string dependentProperty, object targetValue, OperatorType operatorType)
            : this()
        {
            this.TargetValue = targetValue ?? throw new ArgumentNullException(nameof(targetValue), "targetValue Cannot be null");
            this.OperatorType = operatorType;
            this.ErrorMessage = "value is required";
            this.DependentProperty = dependentProperty ?? throw new ArgumentNullException(nameof(dependentProperty), "dependentProperty cannot be null");
        }

        public static Func<object, object, bool, string, ValidationResult> ValidateLimitValue { get; set; }

        public static Func<object, object, object, string, ValidationResult> ValidateRange { get; set; }

        public string DependentProperty { get; set; }

        public object TargetValue { get; set; }

        public OperatorType OperatorType { get; set; }

        public object MinRange { get; set; }

        public object MaxRange { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var containerType = validationContext.ObjectInstance.GetType();
            var field = containerType.GetProperty(this.DependentProperty);
            if (field != null)
            {
                var dependentCurrentValue = field.GetValue(validationContext.ObjectInstance, null);

                if (Comparer.CompareValues(dependentCurrentValue?.ToString(), this.TargetValue?.ToString(), this.OperatorType, Comparer.TypeOfValue(this.TargetValue.ToString())))
                {
                    if (ValidateLimitValue is not null)
                    {
                        return ValidateLimitValue(value, this.MinRange, (bool)this.MaxRange, validationContext.MemberName);
                    }

                    if (ValidateRange is not null)
                    {
                        return ValidateRange(value, this.MinRange, this.MaxRange, validationContext.MemberName);
                    }

                    if (value != null && Array.Exists(value.GetType().GetInterfaces(), i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
                    {
                        var valueList = ((System.Collections.IEnumerable)value).Cast<object>();
                        if (!valueList.Any())
                        {
                            return new ValidationResult(this.ErrorMessage, new[] { validationContext.MemberName });
                        }
                    }

                    if (value is null || (value is string && string.IsNullOrWhiteSpace((string)value)))
                    {
                        return new ValidationResult(this.ErrorMessage, new[] { validationContext.MemberName });
                    }
                }
            }

            return ValidationResult.Success;
        }
    }
}
