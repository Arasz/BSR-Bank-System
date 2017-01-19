using System.Linq;
using System.Net;
using System.ServiceModel.Web;
using Core.Common.Exceptions;
using FluentValidation;
using Service.Contracts;
using Service.Dto;
using Service.InterbankTransfer.Mapping;

namespace Service.InterbankTransfer.Implementation
{
    public class InterbankTransferService : IInterbankTransferService
    {
        private readonly IBankService _bankService;
        private readonly IMapperProvider _mapperProvider;
        private readonly IValidator<InterbankTransferDescription> _validator;

        public InterbankTransferService(IBankService bankService, IValidator<InterbankTransferDescription> validator, IMapperProvider mapperProvider)
        {
            _bankService = bankService;
            _validator = validator;
            _mapperProvider = mapperProvider;
        }

        public void Transfer(InterbankTransferDescription transferDescription)
        {
            var validationResult = _validator.Validate(transferDescription);

            if (!validationResult.IsValid)
            {
                var validationError = validationResult.Errors.First();
                throw new WebFaultException<TransferDataFormatException>(
                    new TransferDataFormatException(validationError.PropertyName, validationError.ErrorMessage, ""),
                    HttpStatusCode.BadRequest);
            }

            _bankService.ExternalTransfer(_mapperProvider.Mapper.Map<TransferDescription>(transferDescription));
        }
    }
}