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

            var allProperties = toValidate.GetAllPublicPropertyNamesToValidate();
            var memberValues = toValidate.GetPropertyValues();

            //return some sort of dictionary with member name and error or something
            return new DataValidationResult(allProperties
                .ToDictionary(p => p.Name, p=> ValidateProperty(toValidate, p.Name, memberValues[p.Name])));
        }

        public static PropertyValidationResult ValidateProperty<TValidate>(TValidate toValidate, string memberName, object value)
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
                ValidationResults = propertyResults
            };
        }


        internal static IEnumerable<PropertyInfo> GetAllPublicPropertyNamesToValidate<TValidate>(this TValidate toValidate)
        {
            var type = toValidate.GetType();
            return type.GetProperties(BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance).AsEnumerable();
        }

        internal static IDictionary<string, object> GetPropertyValues<TValidate>(this TValidate toValidate)
        {
            var valueLookup = toValidate
                .GetAllPublicPropertyNamesToValidate()
                .ToDictionary(p => p.Name, p => p.GetValue(toValidate));
            return valueLookup;
        }
    }
}