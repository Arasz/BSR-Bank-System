using AccountMangmentService.Service;
using FluentValidation;
using InterbankTransactionService.DataStructures;
using Shared.Exceptions;
using System.Net;
using System.ServiceModel.Web;

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

        /// <exception cref="WebFaultException"> Condition. </exception>
        public void Transfer(InterbankTransferDescription transferDescription)
        {
            var validationResult = _validator.Validate(transferDescription);

            ///TODO: Add validator cache (singielton in IOC)
            ///TODO: Create validation groups (for account numbers  and amount)
            ///TODO: Read docs to the end https://github.com/JeremySkinner/FluentValidation/wiki/b.-Creating-a-Validator#chaining-validators-for-the-same-property

            if (!validationResult.IsValid)
                throw new WebFaultException<TransferDataFormatException>(new TransferDataFormatException(), HttpStatusCode.BadRequest);

            throw new System.NotImplementedException();
        }
    }
}