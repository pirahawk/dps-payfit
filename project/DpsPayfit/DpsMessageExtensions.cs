using System;
using System.Collections.Generic;
using System.Linq;
using DpsPayfit.Validation;

namespace DpsPayfit
{
    public static class DpsMessageExtensions
    {
        public static DataValidationResult Validate<TDpsMessage>(this TDpsMessage toValidate) where TDpsMessage: IDpsMessage
        {
            if (toValidate == null) throw new ArgumentNullException(nameof(toValidate));
            var allProperties = toValidate.GetAllPublicPropertyNamesToValidate();
            var memberValues = toValidate.GetPropertyValues();
            return new DataValidationResult(allProperties
                .ToDictionary(p => p.Name, p => DataAnnotationsValidator.ValidateProperty(toValidate, p.Name, memberValues[p.Name])));
        }
    }

    public class DataValidationResult : Dictionary<string, PropertyValidationResult>
    {
        public bool IsValid
        {
            get { return Values.All(v => v.IsValid); }
        }

        public DataValidationResult(IDictionary<string, PropertyValidationResult> copyFrom) : base(copyFrom)
        {
        }

        public DataValidationResult() : base()
        {
        }
    }
}