using AccountMangementService.Service;
using FluentValidation;
using InterbankTransactionService.Dto;
using InterbankTransactionService.Mapping;
using Shared.Exceptions;
using Shared.Transfer;
using System.Linq;
using System.Net;
using System.ServiceModel.Web;

namespace InterbankTransactionService.Service
{
    public class InterbankTransactionServiceImpl : IInterbankTransactionService
    {
        private readonly IAccountManagementService _accountManagementService;
        private readonly IMapping _mapping;
        private readonly IValidator<InterbankTransferDescription> _validator;

        public InterbankTransactionServiceImpl(IAccountManagementService accountManagementService, IValidator<InterbankTransferDescription> validator, IMapping mapping)
        {
            _accountManagementService = accountManagementService;
            _validator = validator;
            _mapping = mapping;
        }

        public void Transfer(InterbankTransferDescription transferDescription)
        {
            var validationResult = _validator.Validate(transferDescription);

            if (!validationResult.IsValid)
            {
                var validationError = validationResult.Errors.First();
                throw new WebFaultException<TransferDataFormatException>(new TransferDataFormatException(validationError.PropertyName, validationError.ErrorMessage, ""), HttpStatusCode.BadRequest);
            }

            _accountManagementService.ExternalTransfer(_mapping.Mapper.Map<TransferDescription>(transferDescription));
        }
    }
}