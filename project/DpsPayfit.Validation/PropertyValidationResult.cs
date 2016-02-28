using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DpsPayfit.Validation
{
    public class PropertyValidationResult
    {
        public bool IsValid { get; set; }
        public IEnumerable<ValidationResult> ValidationResults { get; set; }
        public string MemberName { get; set; }

        public PropertyValidationResult()
        {
            ValidationResults = Enumerable.Empty<ValidationResult>();
        }
    }
}