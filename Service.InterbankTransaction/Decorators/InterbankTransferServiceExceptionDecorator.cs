using Core.Common.Exceptions;
using Service.Contracts;
using Service.Dto;
using System;
using System.Net;
using System.ServiceModel.Web;

namespace Service.InterbankTransfer.Decorators
{
    public class InterbankTransferServiceExceptionDecorator : IInterbankTransferService
    {
        private readonly IInterbankTransferService _decoratedInterbankTransferService;

        public InterbankTransferServiceExceptionDecorator(IInterbankTransferService decoratedInterbankTransferService)
        {
            _decoratedInterbankTransferService = decoratedInterbankTransferService;
        }

        public void Transfer(InterbankTransferDescription transferDescription)
        {
            try
            {
                _decoratedInterbankTransferService.Transfer(transferDescription);
            }
            catch (ValidationFailedException validationException)
            {
                ThrowWebFaultException(validationException, HttpStatusCode.BadRequest);
            }
            catch (AccountNotFoundException accountNotFoundException)
            {
                ThrowWebFaultException(accountNotFoundException, HttpStatusCode.NotFound);
            }
            catch (Exception genericException)
            {
                ThrowWebFaultException(genericException, HttpStatusCode.InternalServerError);
            }
        }

        private static void ThrowWebFaultException<TException>(TException innerException, HttpStatusCode statusCode)
        {
            Console.WriteLine(innerException);
            throw new WebFaultException<TException>(innerException, statusCode);
        }
    }
}