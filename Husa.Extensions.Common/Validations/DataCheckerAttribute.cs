namespace Husa.Extensions.Common.Validations
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class DataCheckerAttribute : ValidationAttribute
    {
        private static readonly IEnumerable<string> ForbiddenWords = [
            "www", ".com", ".com.", ".net", ".org", "@", "able bodied", "adden", "adult community", "adult living", "adults only", "african",
            "agent", "agile", "alcoholic", "appointment", "approval required", "asian", "bachelor", "bonus", "btsa", "buyer's rep", "call",
            "catholic", "caucasian", "chicano", "chinese", "christian", "colored", "commission", "contact", "contract", "couple", "couples",
            "dot", "employed", "empty nester", "facsimile", "fax", "financ", "fsbo", "handicapped", "healthy only", "hispanic", "http", "impaired",
            "incentive", "independent living", "integrated", "irish", "jewish", "landlord", "latino", "married", "mature couple", "mature individual",
            "mature person", "mentally handicapped", "mentally ill", "mexican", "mormon", "mortgage", "mosque", "newlywed", "no black", "no children",
            "no cripple", "no deaf", "no drinkers", "older person", "one child", "one person", "oriental", "phone", "physically fit", "puerto rican",
            "realtor", "retarded", "seasonal worker", "short sale", "shrine", "single person", "singles only", "smoker", "soc sec ins", "tenant",
            "unemployed", "variable commission", "website", "white only"
        ];

        public DataCheckerAttribute()
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var description = value.ToString();

                var words = (from string word in ForbiddenWords
                            where description.Contains(word)
                            select word).ToList();

                if (words.Any())
                {
                    return new ValidationResult(validationContext.MemberName + " contains forbidden words: " + string.Join(", ", words), new[] { validationContext.MemberName });
                }
            }

            return ValidationResult.Success;
        }
    }
}
