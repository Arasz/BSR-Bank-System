using System.Linq;
using Data.Core;
using Service.Bank.Commands;
using Service.Bank.Operations;
using Service.Dto;

namespace Service.Bank.CommandHandlers.Base
{
    /// <summary>
    /// Base for all command handlers connected with bank operations 
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    public abstract class BankOperationCommandHandler<TCommand>
        where TCommand : TransferCommand
    {
        protected readonly BankDataContext BankDataContext;
        protected TransferDescription _transferDescription;
        protected Account Account;
        private readonly IOperationRegister _operationRegister;

        protected BankOperationCommandHandler(BankDataContext bankDataContext, IOperationRegister operationRegister)
        {
            BankDataContext = bankDataContext;
            _operationRegister = operationRegister;
        }

        public virtual void HandleCommand(TCommand command)
        {
            _transferDescription = command.TransferDescription;

            LoadAccount(_transferDescription.From);

            ValidateAccountBalance(_transferDescription.Amount);

            ChangeAccountBalance(_transferDescription.Amount);

            SaveChanges();

            RegisterOperation();
        }

        protected abstract void ChangeAccountBalance(decimal amount);

        protected virtual void LoadAccount(string accountNumber) => Account =
            BankDataContext.Accounts.Single(account => account.Number == accountNumber);

        protected void RegisterOperation() => _operationRegister.RegisterOperation<TCommand>(Account, _transferDescription);

        protected virtual void SaveChanges() => BankDataContext.SaveChanges();

        protected virtual void ValidateAccountBalance(decimal amount)
        {
        }
    }
}