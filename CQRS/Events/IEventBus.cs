namespace CQRS.Events
{
    public interface IEventBus
    {
        /// <summary>
        /// Publishes given event to all subscribers 
        /// </summary>
        /// <typeparam name="TEvent"> Event type </typeparam>
        /// <param name="event"> Published event </param>
        void Publish<TEvent>(TEvent @event)
            where TEvent : IEvent;
    }
}