using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;

namespace CQRS.Validation.Adapters
{
    public class FluentValidatorAdapter<T> : IValidation<T>
    {
        private readonly IValidator<T> _fluentValidator;

        public FluentValidatorAdapter(IValidator<T> fluentValidator)
        {
            _fluentValidator = fluentValidator;
        }

        public void Validate(T validated)
        {
            var validationResult = _fluentValidator.Validate(validated);

            if (!validationResult.IsValid)
                throw new ValidationException(ParseValidationFailures(validationResult.Errors));
        }

        protected virtual string[] ParseValidationFailures(IEnumerable<ValidationFailure> validationFailures)
        {
            return validationFailures
                .Select(failure => $"Validation error on property {failure.PropertyName}. Error: {failure.ErrorMessage}")
                .ToArray();
        }
    }
}