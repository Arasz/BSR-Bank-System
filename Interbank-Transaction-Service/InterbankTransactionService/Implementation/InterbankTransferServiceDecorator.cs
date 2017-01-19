using System;
using System.Net;
using System.ServiceModel.Web;
using Core.Common.Exceptions;
using Service.Contracts;
using Service.Dto;

namespace Service.InterbankTransfer.Implementation
{
    public class InterbankTransferServiceDecorator : IInterbankTransferService
    {
        private readonly IInterbankTransferService _decoratedInterbankTransferService;

        public InterbankTransferServiceDecorator(IInterbankTransferService decoratedInterbankTransferService)
        {
            _decoratedInterbankTransferService = decoratedInterbankTransferService;
        }

        public void Transfer(InterbankTransferDescription transferDescription)
        {
            try
            {
                _decoratedInterbankTransferService.Transfer(transferDescription);
            }
            catch (TransferDescriptionException transferDataFormatException)
            {
                ThrowWebFaultException(transferDataFormatException, HttpStatusCode.BadRequest);
            }
            catch (Exception genericException)
            {
                ThrowWebFaultException(genericException, HttpStatusCode.InternalServerError);
            }
        }

        private static void ThrowWebFaultException<TException>(TException innerException, HttpStatusCode statusCode)
        {
            throw new WebFaultException<TException>(innerException, statusCode);
        }
    }
}