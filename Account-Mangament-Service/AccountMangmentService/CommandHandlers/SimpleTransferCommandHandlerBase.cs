using System.Linq;
using CQRS.Commands;
using Data.Core;
using Service.Bank.Commands;

namespace Service.Bank.CommandHandlers
{
    public abstract class SimpleTransferCommandHandlerBase<TCommand> : ICommandHandler<TCommand>
        where TCommand : TransferCommand
    {
        protected readonly BankDataContext BankDataContext;

        protected Account Account;

        protected SimpleTransferCommandHandlerBase(BankDataContext bankDataContext)
        {
            BankDataContext = bankDataContext;
        }

        public virtual void HandleCommand(TCommand command)
        {
            var transferDescription = command.TransferDescription;

            FetchAccount(transferDescription.From);

            ValidateAccountBalance(transferDescription.Amount);

            ChangeAccountBalance(transferDescription.Amount);

            SaveChanges();
        }

        protected abstract void ChangeAccountBalance(decimal amount);

        protected virtual void FetchAccount(string accountNumber)
        {
            Account = BankDataContext.Accounts.Single(account => account.Number == accountNumber);
        }

        protected virtual void SaveChanges()
        {
            BankDataContext.SaveChanges();
        }

        protected virtual void ValidateAccountBalance(decimal amount)
        {
        }
    }
}