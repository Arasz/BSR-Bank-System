using CQRS.Commands;
using Data.Core;
using Service.Bank.Commands;
using Service.Bank.Exceptions;

namespace Service.Bank.CommandHandlers
{
    public class InternalTransferCommandHandler : BankOperationCommandHandler<InternalTransferCommand>
    {
        public InternalTransferCommandHandler(BankDataContext bankDataContext, ICommandBus commandBus) : base(bankDataContext, commandBus)
        {
        }

        public override void HandleCommand(InternalTransferCommand command)
        {
            DecreaseSenderBalance(command);

            IncreaseReceiverBalance(command);

            SaveChanges();
        }

        protected override void ChangeAccountBalance(decimal amount)
        {
            Account.Balance += amount;
        }

        protected override void ValidateAccountBalance(decimal amount)
        {
            if (Account.Balance < amount)
                throw new AccountBalanceToLowException(Account.Number, Account.Balance, amount);
        }

        private void DecreaseSenderBalance(TransferCommand command)
        {
            var transferDescription = command.TransferDescription;

            LoadAccount(transferDescription.From);
            ValidateAccountBalance(transferDescription.Amount);

            RecordBalance();

            ChangeAccountBalance(-transferDescription.Amount);

            CalculateCreditAndDebit();

            RegisterExecutedOperation();
        }

        private void IncreaseReceiverBalance(TransferCommand command)
        {
            var transferDescription = command.TransferDescription;

            LoadAccount(transferDescription.To);

            RecordBalance();

            ChangeAccountBalance(transferDescription.Amount);

            CalculateCreditAndDebit();

            RegisterExecutedOperation();
        }
    }
}