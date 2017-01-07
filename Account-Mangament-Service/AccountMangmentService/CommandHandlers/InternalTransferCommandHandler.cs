using Data.Core;
using Service.Bank.Commands;
using Service.Bank.Exceptions;

namespace Service.Bank.CommandHandlers
{
    public class InternalTransferCommandHandler : SimpleTransferCommandHandlerBase<InternalTransferCommand>
    {
        public InternalTransferCommandHandler(BankDataContext bankDataContext) : base(bankDataContext)
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
            FetchAccount(command.From);
            ValidateAccountBalance(command.Amount);
            ChangeAccountBalance(-command.Amount);
        }

        private void IncreaseReceiverBalance(TransferCommand command)
        {
            FetchAccount(command.To);
            ChangeAccountBalance(command.Amount);
        }
    }
}