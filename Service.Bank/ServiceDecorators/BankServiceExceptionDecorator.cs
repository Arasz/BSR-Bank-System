using Core.Common.Exceptions;
using Data.Core.Entities;
using FluentValidation;
using Service.Contracts;
using Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace Service.Bank.ServiceDecorators
{
    public class BankServiceExceptionDecorator : IBankService
    {
        private readonly IBankService _bankService;

        public BankServiceExceptionDecorator(IBankService bankService)
        {
            _bankService = bankService;
        }

        public void Deposit(string accountNumber, decimal amount)
        {
            try
            {
                _bankService.Deposit(accountNumber, amount);
            }
            catch (ValidationException validationException)
            {
                ThrowFaultException(ConvertValidationException(validationException));
            }
            catch (Exception genericException)
            {
                ThrowFaultException(genericException);
            }
        }

        public void ExternalTransfer(TransferDescription transferDescription)
        {
            try
            {
                _bankService.ExternalTransfer(transferDescription);
            }
            catch (ValidationException validationException)
            {
                ThrowFaultException(ConvertValidationException(validationException));
            }
            catch (Exception genericException)
            {
                ThrowFaultException(genericException);
            }
        }

        public void InternalTransfer(TransferDescription transferDescription)
        {
            try
            {
                _bankService.InternalTransfer(transferDescription);
            }
            catch (ValidationException validationException)
            {
                ThrowFaultException(ConvertValidationException(validationException));
            }
            catch (Exception genericException)
            {
                ThrowFaultException(genericException);
            }
        }

        public User Login(string userName, string password)
        {
            try
            {
                return _bankService.Login(userName, password);
            }
            catch (ValidationException validationException)
            {
                ThrowFaultException(ConvertValidationException(validationException));
            }
            catch (Exception genericException)
            {
                ThrowFaultException(genericException);
            }

            return User.NullUser;
        }

        public IEnumerable<Operation> OperationsHistory(AccountHistoryQuery accountHistoryQuery)
        {
            try
            {
                return _bankService.OperationsHistory(accountHistoryQuery);
            }
            catch (ValidationException validationException)
            {
                ThrowFaultException(ConvertValidationException(validationException));
            }
            catch (Exception genericException)
            {
                ThrowFaultException(genericException);
            }

            return new List<Operation>();
        }

        public void Withdraw(string accountNumber, decimal amount)
        {
            try
            {
                _bankService.Withdraw(accountNumber, amount);
            }
            catch (ValidationException validationException)
            {
                ThrowFaultException(new ValidationFailedException(validationException.Message));
            }
            catch (Exception genericException)
            {
                ThrowFaultException(genericException);
            }
        }

        private static ValidationFailedException ConvertValidationException(ValidationException validationException)
        {
            var validationFailureMessage = validationException.Errors
                .Select(failure => failure.ErrorMessage)
                .Aggregate("", (accu, failureMessage) => accu += $"{failureMessage}\n");
            return new ValidationFailedException(validationFailureMessage);
        }

        private static string CreateExceptionDescription(Exception innerException) => $"# Exception:\n" +
                                                                                      $"\t- Message: {innerException.Message}\n" +
                                                                                      $"\t- Stack trace: {innerException.StackTrace}\n";

        private void ThrowFaultException(Exception innerException)
        {
            Console.WriteLine(CreateExceptionDescription(innerException));
            throw new FaultException(new FaultReason(innerException.Message));
        }
    }
}