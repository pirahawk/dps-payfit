using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DpsPayfit.Validation
{
    public class DataValidationResult
    {
        public bool IsValid { get; set; }
        public IEnumerable<ValidationResult> ValidationResults { get; set; }

        public DataValidationResult()
        {
            ValidationResults = Enumerable.Empty<ValidationResult>();
        }
    }
}