namespace CQRS.Events
{
    public interface IEventHandler
    {
    }

    public interface IEventHandler<in TEvent> : IEventHandler
        where TEvent : IEvent
    {
        void HandleEvent(TEvent @event);
    }
}