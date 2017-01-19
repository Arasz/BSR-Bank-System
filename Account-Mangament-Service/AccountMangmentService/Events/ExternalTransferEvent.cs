using CQRS.Events;
using Service.Dto;

namespace Service.Bank.Events
{
    public abstract class ExternalTransferEvent : IEvent
    {
        public TransferDescription TransferDescription { get; protected set; }

        protected ExternalTransferEvent(TransferDescription transferDescription)
        {
            TransferDescription = transferDescription;
        }
    }
}