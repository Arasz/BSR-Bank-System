using CQRS.Events;

namespace Service.Bank.Events
{
    /// <summary>
    /// Triggered when any operation is executed 
    /// </summary>
    public class OperationExecutedEvent : IEvent
    {
    }
}