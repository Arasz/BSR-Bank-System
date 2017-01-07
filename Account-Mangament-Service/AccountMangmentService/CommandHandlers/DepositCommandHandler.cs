using System;
using Data.Core;
using Service.Bank.Commands;

namespace Service.Bank.CommandHandlers
{
    public class DepositCommandHandler : SimpleTransferCommandHandlerBase<DepositCommand>

    {
        public DepositCommandHandler(BankDataContext bankDataContext) : base(bankDataContext)
        {
        }

        protected override void ChangeAccountBalance(decimal amount) => Account.Balance += amount;
    }
}