using System.Linq;
using CQRS.Commands;
using Data.Core;
using Service.Bank.Commands;

namespace Service.Bank.CommandHandlers
{
    public class BookExternalTransferCommandHandler : ICommandHandler<BookExternalTransferCommand>
    {
        private readonly BankDataContext _dataContext;

        public BookExternalTransferCommandHandler(BankDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void HandleCommand(BookExternalTransferCommand command)
        {
            UpdateAccountBalance(command);
        }

        private void UpdateAccountBalance(BookExternalTransferCommand command)
        {
            var transferDescription = command.TransferDescription;
            var targetAccount = _dataContext.Accounts.Single(account => account.Number == transferDescription.To);
            targetAccount.Balance += transferDescription.Amount;
            _dataContext.SaveChanges();
        }
    }
}