using CQRS.Events;
using Service.Dto;

namespace Service.Bank.Events
{
    public class ExternalTransferReceivedEvent : IEvent
    {
        public TransferDescription TransferDescription { get; }

        public ExternalTransferReceivedEvent(TransferDescription transferDescription)
        {
            TransferDescription = transferDescription;
        }
    }
}