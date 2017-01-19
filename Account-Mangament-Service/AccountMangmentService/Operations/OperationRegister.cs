using System;
using System.Resources;
using CQRS.Commands;
using Data.Core;
using Service.Bank.Extensions;
using Service.Dto;

namespace Service.Bank.Operations
{
    public class OperationRegister : IOperationRegister
    {
        private readonly ResourceManager _resourceManager;

        public OperationRegister(ResourceManager resourceManager)
        {
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
                Source = transferDescription.From,
                Target = transferDescription.To,
                Title = transferDescription.Title,
                Type = CommandTypeString<TCommand>()
            };

            newOperation.CalculateCreditOrDebit();

            account.Operations.Add(newOperation);
        }

        private string CommandTypeString<TCommand>() => _resourceManager.GetString(typeof(TCommand).Name);
    }
}