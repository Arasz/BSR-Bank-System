using CQRS.Commands;
using Data.Core;
using Service.Bank.Commands;

namespace Service.Bank.CommandHandlers
{
    public class WithdrawCommandHandler : SimpleTransferCommandHandlerBase<WithdrawCommand>
    {
        public WithdrawCommandHandler(BankDataContext bankDataContext) : base(bankDataContext)

        {
        }

        protected override void ChangeAccountBalance(decimal amount) => Account.Balance -= amount;
    }
}