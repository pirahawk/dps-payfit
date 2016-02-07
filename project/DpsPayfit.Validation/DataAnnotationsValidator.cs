using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace DpsPayfit.Validation
{
    public static class DataAnnotationsValidator
    {
        public static DataValidationResult Validate<TValidate>(TValidate toValidate)
        {
            if (toValidate == null) throw new ArgumentNullException(nameof(toValidate));

            var allResults = toValidate.GetAllPublicPropertyNamesToValidate()
                .Select(property => ValidateProperty(toValidate, property.Name));
            if (!allResults.All(r => r.IsValid))
                return new DataValidationResult { IsValid = true };
            return new DataValidationResult
            {
                ValidationResults = allResults.Where(r => !r.IsValid).SelectMany(r => r.ValidationResults)
            };
        }

        public static DataValidationResult ValidateProperty<TValidate>(TValidate toValidate, string memberName)
        {
            if (memberName == null) throw new ArgumentNullException(nameof(memberName));

            var context = new ValidationContext(toValidate)
            {
                MemberName = memberName
            };
            var results = new List<ValidationResult>();
            var result = Validator.TryValidateObject(toValidate, context, results);
            return new DataValidationResult
            {
                IsValid = result,
                ValidationResults = results.AsEnumerable()
            };
        }

        static IEnumerable<PropertyInfo> GetAllPublicPropertyNamesToValidate<TValidate>(this TValidate toValidate)
        {
            var type = toValidate.GetType();
            return type.GetProperties(BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance).AsEnumerable();
        }
    }
}