using System;
using System.Collections.Generic;
using System.ServiceModel;
using Core.Common.Exceptions;
using Data.Core;
using FluentValidation;
using Service.Contracts;
using Service.Dto;

namespace Service.Bank.Implementation
{
    public class BankServiceExceptionDecorator : IBankService
    {
        private readonly IBankService _bankService;

        public BankServiceExceptionDecorator(IBankService bankService)
        {
            _bankService = bankService;
        }

        public User Authentication(string userName, string password)
        {
            try
            {
                return _bankService.Authentication(userName, password);
            }
            catch (ValidationException validationException)
            {
                ThrowFaultException(new ValidationFailedException(validationException.Message));
            }
            catch (Exception genericException)
            {
                ThrowFaultException(genericException);
            }

            return User.NullUser;
        }

        public void Deposit(string accountNumber, decimal amount)
        {
            try
            {
                _bankService.Deposit(accountNumber, amount);
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

        public void ExternalTransfer(TransferDescription transferDescription)
        {
            try
            {
                _bankService.ExternalTransfer(transferDescription);
            }
            catch (AccountNotFoundException accountNotFoundException)
            {
                ThrowFaultException(accountNotFoundException);
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

        public void InternalTransfer(TransferDescription transferDescription)
        {
            try
            {
                _bankService.InternalTransfer(transferDescription);
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

        public IEnumerable<Operation> OperationsHistory(AccountHistoryQuery accountHistoryQuery)
        {
            try
            {
                return _bankService.OperationsHistory(accountHistoryQuery);
            }
            catch (ValidationException validationException)
            {
                ThrowFaultException(new ValidationFailedException(validationException.Message));
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

        private void ThrowFaultException<TException>(TException innerException)
            where TException : Exception
        {
            Console.WriteLine(innerException);
            throw new FaultException<TException>(innerException);
        }

        private void ThrowFaultException(Exception innerException)
        {
            Console.WriteLine(innerException);
            throw new FaultException(new FaultReason(innerException.Message));
        }
    }
}