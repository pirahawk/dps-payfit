using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace DpsPayfit.Validation
{
    public class DataAnnotationsValidator : IDataAnnotationsValidator
    {
        public IEnumerable<PropertyValidationResult> Validate<TValidate>(TValidate toValidate)
        {
            if (toValidate == null) throw new ArgumentNullException(nameof(toValidate));
            var propertyValuesToValidate = GetPropertyValues(toValidate);
            return propertyValuesToValidate
                .Select(kvp => ValidateProperty(toValidate, kvp.Key, kvp.Value))
                .ToArray();
        }

        public PropertyValidationResult ValidateProperty<TValidate>(TValidate toValidate, string memberName, object value)
        {
            if (memberName == null) throw new ArgumentNullException(nameof(memberName));
            var context = new ValidationContext(toValidate)
            {
                MemberName = memberName
            };
            var propertyResults = new List<ValidationResult>();
            return new PropertyValidationResult
            {
                IsValid = Validator.TryValidateProperty(value, context, propertyResults),
                MemberName = memberName,
                ValidationResults = propertyResults
            };
        }

        public static IEnumerable<PropertyInfo> GetAllPublicPropertyNamesToValidate<TValidate>(TValidate toValidate)
        {
            var type = toValidate.GetType();
            return type.GetProperties(BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance).AsEnumerable();
        }

        public static IDictionary<string, object> GetPropertyValues<TValidate>(TValidate toValidate)
        {
            var valueLookup = GetAllPublicPropertyNamesToValidate(toValidate)
                .ToDictionary(p => p.Name, p => p.GetValue(toValidate));
            return valueLookup;
        }
    }
}