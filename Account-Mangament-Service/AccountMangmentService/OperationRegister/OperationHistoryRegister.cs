using System;
using AutoMapper;
using CQRS.Commands;
using Data.Core;
using Service.Bank.Commands;

namespace Service.Bank.OperationRegister
{
    public class OperationHistoryRegister<TCommand> : ICommandHandler<RegisterOperationCommand<TCommand>>
        where TCommand : TransferCommand
    {
        private readonly BankDataContext _dataContext;
        private readonly IMapper _mapper;

        public OperationHistoryRegister(IMapper mapper, BankDataContext dataContext)
        {
            _mapper = mapper;
            _dataContext = dataContext;
        }

        public void HandleCommand(RegisterOperationCommand<TCommand> command)
        {
            var operation = _mapper.Map<Operation>(command);

            _dataContext.Operations.Add(operation);

            _dataContext.SaveChanges();
        }
    }
}