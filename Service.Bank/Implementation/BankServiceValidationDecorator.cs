using System.Collections.Generic;
using Data.Core;
using FluentValidation;
using Service.Contracts;
using Service.Dto;

namespace Service.Bank.Implementation
{
    public class BankServiceValidationDecorator : IBankService
    {
        private readonly IValidator<AccountHistoryQuery> _accountHistoryValidator;
        private readonly IBankService _bankService;
        private readonly IValidator<TransferDescription> _transefValidator;

        public BankServiceValidationDecorator(IBankService bankService, IValidator<TransferDescription> transefValidator,
            IValidator<AccountHistoryQuery> accountHistoryValidator)
        {
            _bankService = bankService;
            _transefValidator = transefValidator;
            _accountHistoryValidator = accountHistoryValidator;
        }

        public User Login(string userName, string password)
        {
            return _bankService.Login(userName, password);
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
            => _transefValidator.ValidateAndThrow(transferDescription);
    }
}