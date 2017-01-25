using Core.Common.Exceptions;
using Data.Core.Entities;
using FluentValidation;
using FluentValidation.Results;
using Service.Contracts;
using Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Bank.Decorators
{
    public class BankServiceValidationDecorator : IBankService
    {
        private readonly IBankService _bankService;
        private readonly Func<Type, IValidator> _validationProvider;

        public BankServiceValidationDecorator(IBankService bankService, Func<Type, IValidator> validationProvider)
        {
            _bankService = bankService;
            _validationProvider = validationProvider;
        }

        public void Deposit(string accountNumber, decimal amount)
        {
            ValidateInstance(CreateSimpleTransferDescription(accountNumber, amount));

            _bankService.Deposit(accountNumber, amount);
        }

        public void ExternalTransfer(TransferDescription transferDescription)
        {
            ValidateInstance(transferDescription);

            _bankService.ExternalTransfer(transferDescription);
        }

        public void InternalTransfer(TransferDescription transferDescription)
        {
            ValidateInstance(transferDescription);

            _bankService.InternalTransfer(transferDescription);
        }

        public User Login(string userName, string password) => _bankService.Login(userName, password);

        public IEnumerable<Operation> OperationsHistory(AccountHistoryQuery accountHistoryQuery)
        {
            ValidateInstance(accountHistoryQuery);

            return _bankService.OperationsHistory(accountHistoryQuery);
        }

        public void Withdraw(string accountNumber, decimal amount)
        {
            ValidateInstance(CreateSimpleTransferDescription(accountNumber, amount));

            _bankService.Withdraw(accountNumber, amount);
        }

        private static TransferDescription CreateSimpleTransferDescription(string accountNumber, decimal amount) =>
            new TransferDescription(accountNumber, accountNumber, "", amount);

        private static string ParseValidationResults(ValidationResult result) => result.Errors
            .Select(failure => failure.ToString())
            .Aggregate("", (accu, failure) => accu += $"{failure}\n");

        private void ValidateInstance<TValidated>(TValidated validatedInstance)
        {
            var validator = _validationProvider(typeof(TValidated));

            var validationResult = validator.Validate(validatedInstance);

            if (!validationResult.IsValid)
                throw new ValidationFailedException(ParseValidationResults(validationResult));
        }
    }
}