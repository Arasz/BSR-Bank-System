using System;
using System.Resources;
using AutoMapper;
using Data.Core;
using Service.Bank.Commands;
using Service.Dto;

namespace Service.Bank.Mappings
{
    public abstract class OperationConverterBase<TCommand> : ITypeConverter<RegisterOperationCommand<TCommand>, Operation>
        where TCommand : TransferCommand
    {
        protected readonly ResourceManager _resourceManager;
        protected decimal _balance;
        protected TCommand _command;
        protected TransferDescription _transferDescription;

        protected OperationConverterBase(ResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }

        public Operation Convert(RegisterOperationCommand<TCommand> source, Operation destination, ResolutionContext context)
        {
            _command = source.ExecutedCommand;

            _transferDescription = source.ExecutedCommand.TransferDescription;

            _balance = source.AccountBalance;

            CommonConversions(destination, context);

            CommandSpecificConversion(destination, context);

            return destination;
        }

        protected abstract void CommandSpecificConversion(Operation destination, ResolutionContext context);

        protected virtual void CommonConversions(Operation destination, ResolutionContext context)
        {
            destination.Type = GetOperationName(nameof(TCommand));

            destination.Amount = _transferDescription.Amount;

            destination.CreationDate = DateTime.Now;

            destination.AccountNumber = _transferDescription.From;

            destination.Title = _transferDescription.Title;

            destination.Balance = _balance;
        }

        protected string GetOperationName(string commandType) => _resourceManager.GetString(commandType);
    }
}