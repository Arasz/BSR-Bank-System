using System.Linq;
using System.Net;
using System.ServiceModel.Web;
using Core.Common.Exceptions;
using FluentValidation;
using Service.Contracts;
using Service.Dto;

namespace Service.InterbankTransfer.Implementation
{
    public class InterbankTransferService : IInterbankTransferService
    {
        private readonly IBankService _bankService;
        private readonly IValidator<InterbankTransferDescription> _validator;

        public InterbankTransferService(IBankService bankService, IValidator<InterbankTransferDescription> validator)
        {
            _bankService = bankService;
            _validator = validator;
        }

        public void Transfer(InterbankTransferDescription transferDescription)
        {
            var validationResult = _validator.Validate(transferDescription);

            if (!validationResult.IsValid)
            {
                var validationError = validationResult.Errors.First();
                throw new TransferDescriptionException(validationError.PropertyName, validationError.ErrorMessage);
            }

            _bankService.ExternalTransfer((TransferDescription)transferDescription);
        }
    }
}