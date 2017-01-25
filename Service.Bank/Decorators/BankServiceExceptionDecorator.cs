using Core.Common.Exceptions;
using Data.Core.Entities;
using Service.Contracts;
using Service.Dto;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Service.Bank.Decorators
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
            CatchAndRethrowException(() =>
            {
                _bankService.Deposit(accountNumber, amount);
            });
        }

        public void ExternalTransfer(TransferDescription transferDescription)
        {
            CatchAndRethrowException(() =>
            {
                _bankService.ExternalTransfer(transferDescription);
            });
        }

        public void InternalTransfer(TransferDescription transferDescription)
        {
            CatchAndRethrowException(() =>
            {
                _bankService.InternalTransfer(transferDescription);
            });
        }

        public User Login(string userName, string password)
        {
            return CatchAndRethrowException(() =>
            {
                return _bankService.Login(userName, password);
            });
        }

        public IEnumerable<Operation> OperationsHistory(AccountHistoryQuery accountHistoryQuery)
        {
            return CatchAndRethrowException(() =>
            {
                return _bankService.OperationsHistory(accountHistoryQuery);
            });
        }

        public void Withdraw(string accountNumber, decimal amount)
        {
            CatchAndRethrowException(() =>
            {
                _bankService.Withdraw(accountNumber, amount);
            });
        }

        private static string CreateExceptionDescription(Exception innerException) => $"# Exception:\n" +
                                                                                      $"\t- Message: {innerException.Message}\n" +
                                                                                      $"\n\t- Stack trace: {innerException.StackTrace}\n\n";

        private TReturn CatchAndRethrowException<TReturn>(Func<TReturn> throwingAction)
        {
            try
            {
                return throwingAction();
            }
            catch (ValidationFailedException validationException)
            {
                ThrowFaultException((validationException));
            }
            catch (Exception genericException)
            {
                ThrowFaultException(genericException);
            }

            return default(TReturn);
        }

        private void CatchAndRethrowException(Action throwingAction)
        {
            try
            {
                throwingAction();
            }
            catch (ValidationFailedException validationException)
            {
                ThrowFaultException((validationException));
            }
            catch (Exception genericException)
            {
                ThrowFaultException(genericException);
            }
        }

        private void ThrowFaultException(Exception innerException)
        {
            Console.WriteLine(CreateExceptionDescription(innerException));
            throw new FaultException(new FaultReason(innerException.Message));
        }
    }
}