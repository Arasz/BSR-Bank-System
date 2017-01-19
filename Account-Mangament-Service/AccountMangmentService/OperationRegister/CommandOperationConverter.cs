using System;
using System.Resources;
using Data.Core;
using Service.Bank.Commands;
using Service.Dto;

namespace Service.Bank.OperationRegister
{
    public class CommandOperationConverter : ICommandOperationConverter
    {
        private readonly ResourceManager _resourceManager;
        private decimal _balance;
        private string _sourceCommandName;
        private TransferDescription _transferDescription;

        public CommandOperationConverter(ResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }

        public Operation Convert(RegisterBankOperationCommand registerBankOperationCommand)
        {
            var destination = new Operation();

            _transferDescription = registerBankOperationCommand.TransferDescription;
            _balance = registerBankOperationCommand.AccountBalance;
            _sourceCommandName = registerBankOperationCommand.SourceCommandName;

            destination.Type = GetOperationName(_sourceCommandName);

            destination.Amount = _transferDescription.Amount;

            destination.CreationDate = DateTime.Now;

            destination.AccountNumber = _transferDescription.From;

            destination.Title = _transferDescription.Title;

            destination.Balance = _balance;

            destination.Credit = registerBankOperationCommand.Credit;

            destination.Debit = registerBankOperationCommand.Debit;

            return destination;
        }

        private string GetOperationName(string commandName) => _resourceManager.GetString(commandName);
    }
}