using System;

namespace CQRS.Validation
{
    public class ValidationException : Exception
    {
        public string[] ValidationErrors { get; }

        public ValidationException(string[] validationErrors)
        {
            ValidationErrors = validationErrors;
        }
    }
}