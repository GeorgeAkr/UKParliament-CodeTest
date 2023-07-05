using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UKParliament.CodeTest.Data.Errors;

namespace UKParliament.CodeTest.Data.Validators
{
    public class PersonValidator : IPersonValidator
    {
        public ValidationResult Validate(Person person)
        {
            var result = new ValidationResult() { Errors = new Dictionary<string, string>()};
            if(person == null)
            {
                result.Errors.Add(nameof(Person), PersonErrorDescriptor.NullPersonError);
                return result;
            }

            if(person.Name == null)
            {
                result.Errors.Add(nameof(Person.Name), PersonErrorDescriptor.NullNameError);
            }
            else if (string.IsNullOrWhiteSpace(person.Name))
            {
                result.Errors.Add(nameof(Person.Name), PersonErrorDescriptor.EmptyNameError);
            }
            
            return result;
        }
    }
}
