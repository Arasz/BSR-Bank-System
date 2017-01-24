using Core.CQRS.Commands;
using Data.Core;
using Data.Core.Entities;
using Service.Bank.Extensions;
using Service.Dto;
using System;
using System.Resources;

namespace Service.Bank.Operations
{
    public class OperationRegister : IOperationRegister
    {
        private readonly BankDataContext _dataContext;
        private readonly ResourceManager _resourceManager;

        public OperationRegister(BankDataContext dataContext, ResourceManager resourceManager)
        {
            _dataContext = dataContext;
            _resourceManager = resourceManager;
        }

        public void RegisterOperation<TCommand>(Account account, TransferDescription transferDescription)
            where TCommand : ICommand
        {
            var newOperation = new Operation
            {
                Account = account,
                AccountId = account.Id,
                Amount = transferDescription.Amount,
                Balance = account.Balance,
                CreationDate = DateTime.Now,
                Source = transferDescription.SourceAccountNumber,
                Target = transferDescription.TargetAccountNumber,
                Title = transferDescription.Title,
                Type = CommandTypeString<TCommand>()
            };

            newOperation.CalculateCreditOrDebit();

            account.Operations.Add(newOperation);

            _dataContext.Operations.Add(newOperation);
            _dataContext.SaveChanges();
        }

        private string CommandTypeString<TCommand>() => _resourceManager.GetString(typeof(TCommand).Name);
    }
}