using System.Linq;
using CQRS.Commands;
using Data.Core;
using Service.Bank.Commands;

namespace Service.Bank.CommandHandlers
{
    public class WithdrawCommandHandler : ICommandHandler<WithdrawCommand>
    {
        private readonly BankDataContext _dataContext;

        public WithdrawCommandHandler(BankDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void HandleCommand(WithdrawCommand command)
        {
            var account = _dataContext.Accounts
                .Single(acc => acc.Number == command.From);

            account.Balance -= command.Amount;

            _dataContext.SaveChanges();
        }
    }
}