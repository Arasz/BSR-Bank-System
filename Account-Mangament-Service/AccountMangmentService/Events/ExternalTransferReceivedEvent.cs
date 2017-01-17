using CQRS.Events;
using Shared.Transfer;

namespace Service.Bank.Events
{
    public class ExternalTransferReceivedEvent : IEvent
    {
        public decimal Amount { get; }

        public string From { get; }

        public string Title { get; }

        public string To { get; }

        public ExternalTransferReceivedEvent(TransferDescription receivedTransferDescription)
        {
            From = receivedTransferDescription.SenderAccount;
            To = receivedTransferDescription.ReceiverAccount;
            Amount = receivedTransferDescription.Amount;
            Title = receivedTransferDescription.Title;
        }

        public ExternalTransferReceivedEvent(string from, string to, decimal amount, string title)
        {
            From = from;
            To = to;
            Title = title;
            Amount = amount;
        }
    }
}