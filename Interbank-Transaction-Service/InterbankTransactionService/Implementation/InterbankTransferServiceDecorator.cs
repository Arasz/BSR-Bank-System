using System;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Web;
using Core.Common.Exceptions;
using Service.Bank.Exceptions;
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
            catch (FaultException<ValidationFailedException> validationException)
            {
                ThrowWebFaultException(validationException.Detail, HttpStatusCode.BadRequest);
            }
            catch (FaultException<AccountNotFoundException> accountNotFoundException)
            {
                ThrowWebFaultException(accountNotFoundException.Detail, HttpStatusCode.NotFound);
            }
            catch (FaultException faultException)
            {
                ThrowWebFaultException(new Exception(faultException.Reason.ToString()), HttpStatusCode.InternalServerError);
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