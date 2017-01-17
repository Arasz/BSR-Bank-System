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
            var targetAccount = _dataContext.Accounts.Single(account => account.Number == @event.To);
            targetAccount.Balance += @event.Amount;
            _dataContext.SaveChanges();
        }
    }
}