namespace CQRS.Events
{
    public interface IEventHandler
    {
    }

    /// <summary>
    /// Handles given event 
    /// </summary>
    /// <typeparam name="TEvent"> Event type </typeparam>
    public interface IEventHandler<in TEvent> : IEventHandler
        where TEvent : IEvent
    {
        void HandleEvent(TEvent @event);
    }
}