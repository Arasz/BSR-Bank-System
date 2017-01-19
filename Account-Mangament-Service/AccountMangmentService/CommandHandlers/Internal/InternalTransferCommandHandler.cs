using Data.Core;
using Service.Bank.CommandHandlers.Base;
using Service.Bank.Commands;
using Service.Bank.Exceptions;
using Service.Bank.Operations;

namespace Service.Bank.CommandHandlers.Internal
{
    public class InternalTransferCommandHandler : BankOperationCommandHandler<InternalTransferCommand>
    {
        public InternalTransferCommandHandler(BankDataContext bankDataContext, IOperationRegister register) : base(bankDataContext, register)
        {
        }

        public override void HandleCommand(InternalTransferCommand command)
        {
            _transferDescription = command.TransferDescription;

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

            ChangeAccountBalance(-transferDescription.Amount);

            RegisterOperation();
        }

        private void IncreaseReceiverBalance(TransferCommand command)
        {
            var transferDescription = command.TransferDescription;

            LoadAccount(transferDescription.To);

            ChangeAccountBalance(transferDescription.Amount);

            RegisterOperation();
        }
    }
}