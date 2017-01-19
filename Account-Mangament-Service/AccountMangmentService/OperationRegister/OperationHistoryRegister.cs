using System;
using AutoMapper;
using CQRS.Commands;
using Data.Core;
using Service.Bank.Commands;

namespace Service.Bank.OperationRegister
{
    public class OperationHistoryRegister : ICommandHandler<RegisterBankOperationCommand>
    {
        private readonly ICommandOperationConverter _commandOperationConverter;
        private readonly BankDataContext _dataContext;

        public OperationHistoryRegister(ICommandOperationConverter commandOperationConverter, BankDataContext dataContext)
        {
            _commandOperationConverter = commandOperationConverter;
            _dataContext = dataContext;
        }

        public void HandleCommand(RegisterBankOperationCommand command)
        {
            var operation = _commandOperationConverter.Convert(command);

            _dataContext.Operations.Add(operation);

            _dataContext.SaveChanges();
        }
    }
}