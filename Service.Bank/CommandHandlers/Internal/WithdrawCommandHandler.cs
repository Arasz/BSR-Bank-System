using Data.Core;
using Service.Bank.CommandHandlers.Base;
using Service.Bank.Commands;
using Service.Bank.Exceptions;
using Service.Bank.Operations;

namespace Service.Bank.CommandHandlers.Internal
{
    public class WithdrawCommandHandler : BankOperationCommandHandler<WithdrawCommand>
    {
        public WithdrawCommandHandler(BankDataContext bankDataContext, IOperationRegister register)
            : base(bankDataContext, register)
        {
        }

        protected override void ValidateAccountBalance(decimal amount)
        {
            if (Account.Balance < amount)
                throw new AccountBalanceToLowException(Account.Number, Account.Balance, amount);
        }
    }
}