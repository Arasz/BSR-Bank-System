using Data.Core;
using Service.Bank.CommandHandlers.Base;
using Service.Bank.Commands;
using Service.Bank.Exceptions;
using Service.Bank.Operations;

namespace Service.Bank.CommandHandlers.Internal
{
    public class InternalTransferCommandHandler : BankOperationCommandHandler<InternalTransferCommand>
    {
        public InternalTransferCommandHandler(BankDataContext bankDataContext, IOperationRegister register)
            : base(bankDataContext, register)
        {
        }

        public override void HandleCommand(InternalTransferCommand command)
        {
            _transferDescription = command.TransferDescription;

            DecreaseSenderBalance();

            IncreaseReceiverBalance();

            SaveChanges();
        }

        protected override void ValidateAccountBalance(decimal amount)
        {
            if (_transferDescription.From == Account.Number && Account.Balance < amount)
                throw new AccountBalanceToLowException(Account.Number, Account.Balance, amount);
        }

        private void DecreaseSenderBalance()
        {
            UpdateAccountBalance(_transferDescription.Amount, _transferDescription.From);

            RegisterOperation();
        }

        private void IncreaseReceiverBalance()
        {
            UpdateAccountBalance(-_transferDescription.Amount, _transferDescription.To);

            RegisterOperation();
        }
    }
}