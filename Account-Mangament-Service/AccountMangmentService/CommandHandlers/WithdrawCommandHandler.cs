using CQRS.Commands;
using Data.Core;
using Service.Bank.Commands;
using Service.Bank.Exceptions;

namespace Service.Bank.CommandHandlers
{
    public class WithdrawCommandHandler : SimpleTransferCommandHandlerBase<WithdrawCommand>
    {
        public WithdrawCommandHandler(BankDataContext bankDataContext) : base(bankDataContext)

        {
        }

        protected override void ChangeAccountBalance(decimal amount) => Account.Balance -= amount;

        protected override void ValidateAccountBalance(decimal amount)
        {
            if (Account.Balance < amount)
                throw new AccountBalanceToLowException(Account.Number, Account.Balance, amount);
        }
    }
}