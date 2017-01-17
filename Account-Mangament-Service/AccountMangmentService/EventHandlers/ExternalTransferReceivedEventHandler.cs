using CQRS.Events;
using Service.Bank.Events;

namespace Service.Bank.EventHandlers
{
    public class ExternalTransferReceivedEventHandler : IEventHandler<ExternalTransferReceivedEvent>
    {
        public void HandleEvent(ExternalTransferReceivedEvent @event)
        {
            throw new System.NotImplementedException();
        }
    }
}