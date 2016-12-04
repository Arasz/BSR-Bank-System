using System;
using System.Collections.Generic;
using System.Linq;

namespace CQRS.Events
{
    internal class EventBus : IEventBus
    {
        private readonly Func<Type, IEnumerable<IEventHandler>> _eventHandlersFactory;

        public EventBus(Func<Type, IEnumerable<IEventHandler>> eventHandlersFactory)
        {
            _eventHandlersFactory = eventHandlersFactory;
        }

        public void Publish<TEvent>(TEvent @event) where TEvent : IEvent
        {
            if (@event == null)
                throw new ArgumentException("Event can not be null");

            var eventHandlers = _eventHandlersFactory(typeof(TEvent))
                .Cast<IEventHandler<TEvent>>()
                .ToList();

            foreach (var eventHandler in eventHandlers)
                eventHandler.HandleEvent(@event);
        }
    }
}