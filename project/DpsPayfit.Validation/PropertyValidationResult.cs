using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DpsPayfit.Validation
{
    public class PropertyValidationResult
    {
        public bool IsValid { get; set; }
        public IEnumerable<ValidationResult> ValidationResults { get; set; }

        //public IEnumerable<DataValidationError> ValidationResults { get; set; }

        public PropertyValidationResult()
        {
            ValidationResults = Enumerable.Empty<ValidationResult>();
        }
    }

    public class DataValidationResult: Dictionary<string, PropertyValidationResult>
    {
        public bool IsValid
        {
            get { return Values.All(v => v.IsValid); }
        }

        public DataValidationResult(IDictionary<string, PropertyValidationResult> copyFrom):base(copyFrom)
        {
        }

        public DataValidationResult(): base()
        {            
        }
    }

    
}