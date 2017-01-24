using Data.Core.Entities;
using FluentValidation;
using Service.Contracts;
using Service.Dto;
using System.Collections.Generic;

namespace Service.Bank.ServiceDecorators
{
    public class BankServiceValidationDecorator : IBankService
    {
        private readonly IValidator<AccountHistoryQuery> _accountHistoryValidator;
        private readonly IBankService _bankService;
        private readonly IValidator<TransferDescription> _transeferValidator;

        public BankServiceValidationDecorator(IBankService bankService, IValidator<TransferDescription> transeferValidator,
            IValidator<AccountHistoryQuery> accountHistoryValidator)
        {
            _bankService = bankService;
            _transeferValidator = transeferValidator;
            _accountHistoryValidator = accountHistoryValidator;
        }

        public void Deposit(string accountNumber, decimal amount)
        {
            ValidateTransferDescription(new TransferDescription(accountNumber, accountNumber, "", amount));
            _bankService.Deposit(accountNumber, amount);
        }

        public void ExternalTransfer(TransferDescription transferDescription)
        {
            ValidateTransferDescription(transferDescription);
            _bankService.ExternalTransfer(transferDescription);
        }

        public void InternalTransfer(TransferDescription transferDescription)
        {
            ValidateTransferDescription(transferDescription);
            _bankService.InternalTransfer(transferDescription);
        }

        public User Login(string userName, string password)
        {
            return _bankService.Login(userName, password);
        }

        public IEnumerable<Operation> OperationsHistory(AccountHistoryQuery accountHistoryQuery)
        {
            _accountHistoryValidator.ValidateAndThrow(accountHistoryQuery);

            return _bankService.OperationsHistory(accountHistoryQuery);
        }

        public void Withdraw(string accountNumber, decimal amount)
        {
            ValidateTransferDescription(new TransferDescription(accountNumber, accountNumber, "", amount));
            _bankService.Withdraw(accountNumber, amount);
        }

        private void ValidateTransferDescription(TransferDescription transferDescription)
            => _transeferValidator.ValidateAndThrow(transferDescription);
    }
}