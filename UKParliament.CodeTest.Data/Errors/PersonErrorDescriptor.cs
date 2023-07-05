using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UKParliament.CodeTest.Data.Errors
{
    public static class PersonErrorDescriptor
    {
        public const string NullPersonError = "Person cannot be null.";
        public const string EmptyNameError = "Name cannot be empty or whitespace.";
        public const string NullNameError = "Name cannot be null.";
        public const string DateTooFarInThePastError = "Date of birth is too far in the past.";
        public const string DateInTheFutureError = "Date of birth is too far in the past.";

    }
}
