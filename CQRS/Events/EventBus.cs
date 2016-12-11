﻿using CQRS.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CQRS.Events
{
    public class EventBus : IEventBus
    {
        private readonly Func<Type, IEnumerable<IEventHandler>> _eventHandlersFactory;

        public EventBus(Func<Type, IEnumerable<IEventHandler>> eventHandlersFactory)
        {
            _eventHandlersFactory = eventHandlersFactory;
        }

        /// <exception cref="ArgumentException"> Event can not be null </exception>
        /// <exception cref="NullHandlerException"> Events handler factory returned null </exception>
        public void Publish<TEvent>(TEvent @event) where TEvent : IEvent
        {
            if (@event == null)
                throw new ArgumentException("Event can not be null");

            var eventHandlers = _eventHandlersFactory(typeof(TEvent))?
                .Cast<IEventHandler<TEvent>>()
                .ToList();

            if (eventHandlers == null)
                throw new NullHandlerException("Events handler factory returned null", typeof(IEventHandler<TEvent>));

            foreach (var eventHandler in eventHandlers)
                eventHandler.HandleEvent(@event);
        }
    }
}