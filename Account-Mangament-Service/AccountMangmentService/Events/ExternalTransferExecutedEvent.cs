using CQRS.Events;
using Service.Dto;

namespace Service.Bank.Events
{
    public class ExternalTransferExecutedEvent : ExternalTransferEvent
    {
        public ExternalTransferExecutedEvent(TransferDescription transferDescription) : base(transferDescription)
        {
        }
    }
}