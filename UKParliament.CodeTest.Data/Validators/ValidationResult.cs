using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UKParliament.CodeTest.Data.Validators
{
    public class ValidationResult : IValidatorResult
    {
        public Dictionary<string, string> Errors { get; set; } = new Dictionary<string, string>();
        public bool IsValid => Errors.Count > 0;

    }
}
