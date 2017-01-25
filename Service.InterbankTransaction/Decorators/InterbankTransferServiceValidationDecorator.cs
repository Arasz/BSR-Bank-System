using Core.Common.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using Service.Contracts;
using Service.Dto;
using System.Linq;

namespace Service.InterbankTransfer.Decorators
{
    public class InterbankTransferServiceValidationDecorator : IInterbankTransferService
    {
        private readonly IInterbankTransferService _interbankTransferService;
        private readonly IValidator<InterbankTransferDescription> _validator;

        public InterbankTransferServiceValidationDecorator(IInterbankTransferService interbankTransferService, IValidator<InterbankTransferDescription> validator)
        {
            _interbankTransferService = interbankTransferService;
            _validator = validator;
        }

        public void Transfer(InterbankTransferDescription transferDescription)
        {
            var result = _validator.Validate(transferDescription);

            if (!result.IsValid)
                throw new ValidationFailedException(ParseValidationResults(result));

            _interbankTransferService.Transfer(transferDescription);
        }

        private static string ParseValidationResults(ValidationResult result) => result.Errors
            .Select(failure => failure.ToString())
            .Aggregate("", (accu, failure) => accu += $"{failure}\n");
    }
}