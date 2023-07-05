using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UKParliament.CodeTest.Data.Validators
{
    public interface IPersonValidator
    {
        public ValidationResult Validate(Person person);
    }
}
