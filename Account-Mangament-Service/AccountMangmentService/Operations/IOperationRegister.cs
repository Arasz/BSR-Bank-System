using Core.CQRS.Commands;
using Data.Core;
using Service.Dto;

namespace Service.Bank.Operations
{
    public interface IOperationRegister
    {
        void RegisterOperation<TCommand>(Account account, TransferDescription transferDescription) where TCommand : ICommand;
    }
}