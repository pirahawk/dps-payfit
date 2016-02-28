using System.Collections.Generic;

namespace DpsPayfit.Validation
{
    public interface IDataAnnotationsValidator
    {
        IEnumerable<PropertyValidationResult> Validate<TValidate>(TValidate toValidate);
    }
}