using Core.Common.Exceptions;
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

        /// <summary>
        /// Makes transfer to account in the same bank 
        /// </summary>
        public override void HandleCommand(InternalTransferCommand command)
        {
            _transferDescription = command.TransferDescription;

            ValidateAccounts();

            DecreaseSenderBalance();

            IncreaseReceiverBalance();

            SaveChanges();
        }

        protected override void ValidateAccountBalance(decimal amount)
        {
            if (_transferDescription.SenderAccountNumber == Account.Number && Account.Balance < amount)
                throw new AccountBalanceToLowException(Account.Number, Account.Balance, amount);
        }

        private void DecreaseSenderBalance()
        {
            UpdateAccountBalance(_transferDescription.Amount, _transferDescription.SenderAccountNumber);

            RegisterOperation();
        }

        private void IncreaseReceiverBalance()
        {
            UpdateAccountBalance(-_transferDescription.Amount, _transferDescription.ReceiverAccountNumber);

            RegisterOperation();
        }

        private void ValidateAccounts()
        {
            if (!_transferDescription.ReceiverAccountNumber.Contains("112241"))
                throw new AccountNotFoundException("Receiver account is from different bank");

            if (!_transferDescription.SenderAccountNumber.Contains("112241"))
                throw new AccountNotFoundException("Sender account is from different bank");
        }
    }
}