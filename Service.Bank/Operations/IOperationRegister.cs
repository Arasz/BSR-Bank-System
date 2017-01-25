using Core.CQRS.Commands;
using Data.Core.Entities;
using Service.Dto;

namespace Service.Bank.Operations
{
    public interface IOperationRegister
    {
        /// <summary>
        /// Registers bank operation (command) for given account 
        /// </summary>
        /// <typeparam name="TCommand"> Registered command type </typeparam>
        /// <param name="account"> Account connected with operation </param>
        /// <param name="transferDescription"> Operation data </param>
        void RegisterOperation<TCommand>(Account account, TransferDescription transferDescription)
            where TCommand : ICommand;
    }
}