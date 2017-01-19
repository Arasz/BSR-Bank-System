using Data.Core;
using Service.Bank.CommandHandlers.Base;
using Service.Bank.Commands;
using Service.Bank.Operations;

namespace Service.Bank.CommandHandlers.Internal
{
    public class DepositCommandHandler : BankOperationCommandHandler<DepositCommand>

    {
        public DepositCommandHandler(BankDataContext bankDataContext, IOperationRegister register) : base(bankDataContext, register)
        {
        }

        protected override void ChangeAccountBalance(decimal amount) => Account.Balance += amount;
    }
}