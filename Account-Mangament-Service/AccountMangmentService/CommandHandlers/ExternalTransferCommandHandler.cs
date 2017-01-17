using System.Linq;
using CQRS.Commands;
using Data.Core;
using Service.Bank.Commands;

namespace Service.Bank.CommandHandlers
{
    public class ExternalTransferCommandHandler : ICommandHandler<ExternalTransferCommand>
    {
        private readonly BankDataContext _dataContext;

        public ExternalTransferCommandHandler(BankDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void HandleCommand(ExternalTransferCommand command)
        {
            var targetAccount = _dataContext.Accounts.Single(account => account.Number == command.To);
            targetAccount.Balance += command.Amount;
            _dataContext.SaveChanges();
        }
    }
}