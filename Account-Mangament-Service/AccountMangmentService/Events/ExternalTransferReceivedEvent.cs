using CQRS.Events;
using Shared.Transfer;

namespace Service.Bank.Events
{
    public class ExternalTransferReceivedEvent : IEvent
    {
        public TransferDescription ReceivedTransferDescription { get; }

        public ExternalTransferReceivedEvent(TransferDescription receivedTransferDescription)
        {
            ReceivedTransferDescription = receivedTransferDescription;
        }
    }
}