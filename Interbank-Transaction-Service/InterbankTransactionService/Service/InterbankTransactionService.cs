using AccountMangmentService.Service;
using FluentValidation;
using InterbankTransactionService.DataStructures;
using InterbankTransactionService.Exceptions;
using System.Linq;

namespace InterbankTransactionService.Service
{
    public class InterbankTransactionService : IInterbankTransactionService
    {
        private readonly IAccountManagmentService _accountManagmentService;
        private readonly IValidator<InterbankTransferDescription> _validator;

        public InterbankTransactionService(IAccountManagmentService accountManagmentService, IValidator<InterbankTransferDescription> validator)
        {
            _accountManagmentService = accountManagmentService;
            _validator = validator;
        }

        /// <exception cref="TransferValidationException"> Errors during validation. </exception>
        public void Transfer(InterbankTransferDescription transferDescription)
        {
            var validationResult = _validator.Validate(transferDescription);

            ///TODO: Add validator cache (singielton in IOC)
            ///TODO: Create validation groups (for account numbers  and amount)
            ///TODO: Read docs to the end https://github.com/JeremySkinner/FluentValidation/wiki/b.-Creating-a-Validator#chaining-validators-for-the-same-property

            if (!validationResult.IsValid)
            {
                var errorDictionary = validationResult.Errors.ToDictionary(failure => failure.ErrorMessage,
                    failure => failure.AttemptedValue);
                throw new TransferValidationException(errorDictionary);
            }

            throw new System.NotImplementedException();
        }
    }
}