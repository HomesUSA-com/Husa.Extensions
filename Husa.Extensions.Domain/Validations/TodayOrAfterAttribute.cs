namespace Husa.Extensions.Domain.Validations
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class TodayOrAfterAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime date)
            {
                var isTodayOrAfter = date >= DateTime.Now.Date;
                if (isTodayOrAfter)
                {
                    return ValidationResult.Success;
                }

                return new ValidationResult("Must be on or after today");
            }

            return ValidationResult.Success;
        }
    }
}
