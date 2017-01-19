using CQRS.Events;
using Service.Dto;

namespace Service.Bank.Events
{
    public class ExternalTransferReceivedEvent : ExternalTransferEvent
    {
        public ExternalTransferReceivedEvent(TransferDescription transferDescription) : base(transferDescription)
        {
        }
    }
}