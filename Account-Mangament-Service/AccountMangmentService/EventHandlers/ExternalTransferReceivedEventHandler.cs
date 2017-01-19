using System;
using System.Linq;
using CQRS.Events;
using Data.Core;
using Service.Bank.Events;

namespace Service.Bank.EventHandlers
{
    public class ExternalTransferReceivedEventHandler : IEventHandler<ExternalTransferReceivedEvent>
    {
        private readonly BankDataContext _dataContext;

        public ExternalTransferReceivedEventHandler(BankDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void HandleEvent(ExternalTransferReceivedEvent @event)
        {
            UpdateAccountBalance(@event);
        }

        private void UpdateAccountBalance(ExternalTransferReceivedEvent @event)
        {
            var transferDescription = @event.TransferDescription;
            var targetAccount = _dataContext.Accounts.Single(account => account.Number == transferDescription.To);
            targetAccount.Balance += transferDescription.Amount;
            _dataContext.SaveChanges();
        }
    }
}