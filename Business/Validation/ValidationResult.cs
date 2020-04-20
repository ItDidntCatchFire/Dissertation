using System.Collections.Generic;
using System.Linq;

namespace Business.Validation
{
    public class ValidationResult
    {
        public List<string> Reasons { get; set; } =new List<string>();
        
        public bool IsValid => (Reasons.Count == 0);
        
        public ValidationResult(IEnumerable<string> reasons)
        {
            Reasons = reasons.ToList();
        }
        
        public ValidationResult(params ValidationResult[] validationResults)
        {
            Reasons.AddRange(validationResults.SelectMany(result => result.Reasons));
        }
    }
}