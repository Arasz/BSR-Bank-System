using AccountMangmentService.Service;
using AccountMangmentService.Transfer.Dto;
using AutoMapper;
using FluentValidation;
using InterbankTransactionService.Dto;
using Shared.Exceptions;
using System.Linq;
using System.Net;
using System.ServiceModel.Web;

namespace InterbankTransactionService.Service
{
    public class InterbankTransactionService : IInterbankTransactionService
    {
        private readonly IAccountManagmentService _accountManagementService;
        private readonly IValidator<InterbankTransferDescription> _validator;

        public InterbankTransactionService(IAccountManagmentService accountManagementService, IValidator<InterbankTransferDescription> validator)
        {
            _accountManagementService = accountManagementService;
            _validator = validator;
        }

        public void Transfer(InterbankTransferDescription transferDescription)
        {
            var validationResult = _validator.Validate(transferDescription);

            if (!validationResult.IsValid)
            {
                var validationError = validationResult.Errors.First();
                throw new WebFaultException<TransferDataFormatException>(new TransferDataFormatException(validationError.PropertyName, validationError.ErrorMessage, ""), HttpStatusCode.BadRequest);
            }

            _accountManagementService.ExternalTransfer(Mapper.Map<TransferDescription>(transferDescription));
        }
    }
}