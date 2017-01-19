using System.Linq;
using Core.Common.Calculators;
using CQRS.Commands;
using Data.Core;
using Service.Bank.Commands;
using Service.Dto;

namespace Service.Bank.CommandHandlers
{
    /// <summary>
    /// Base for all command handlers connected with bank operations 
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    public abstract class BankOperationCommandHandler<TCommand>
        where TCommand : TransferCommand
    {
        protected readonly BankDataContext BankDataContext;
        protected readonly ICommandBus CommandBus;

        protected TransferDescription _transferDescription;
        protected Account Account;
        private CreditDebitCalculator _creditDebitCalculator;

        protected BankOperationCommandHandler(BankDataContext bankDataContext, ICommandBus commandBus)
        {
            BankDataContext = bankDataContext;
            CommandBus = commandBus;
        }

        public virtual void HandleCommand(TCommand command)
        {
            _transferDescription = command.TransferDescription;

            LoadAccount(_transferDescription.From);

            ValidateAccountBalance(_transferDescription.Amount);

            RecordBalance();

            ChangeAccountBalance(_transferDescription.Amount);

            CalculateCreditAndDebit();

            SaveChanges();

            RegisterExecutedOperation();
        }

        protected void CalculateCreditAndDebit() => _creditDebitCalculator.CalculateCreditAndDebit(Account.Balance);

        protected abstract void ChangeAccountBalance(decimal amount);

        protected virtual void LoadAccount(string accountNumber) => Account =
            BankDataContext.Accounts.Single(account => account.Number == accountNumber);

        protected void RecordBalance() => _creditDebitCalculator = new CreditDebitCalculator(Account.Balance);

        protected virtual void RegisterExecutedOperation()
        {
            var credit = _creditDebitCalculator.Credit;

            var debit = _creditDebitCalculator.Debit;

            var registerOperationCommand = new RegisterBankOperationCommand(_transferDescription, Account.Balance,
                nameof(TCommand), credit, debit);

            CommandBus.Send(registerOperationCommand);
        }

        protected virtual void SaveChanges() => BankDataContext.SaveChanges();

        protected virtual void ValidateAccountBalance(decimal amount)
        {
        }
    }
}