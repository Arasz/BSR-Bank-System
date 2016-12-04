using Autofac;
using CQRS.Commands;
using CQRS.Events;
using CQRS.Queries;
using CQRS.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CQRS.Bus
{
    public class GenericBus : IBus
    {
        private readonly Func<Type, ICommandHandler> _commandHandlersFactory;
        private readonly IComponentContext _context;
        private readonly Func<Type, IEnumerable<IEventHandler>> _eventHandlersFactory;
        private readonly Func<Type, IQueryHandler> _queryHandlerFactory;
        private readonly Func<Type, IValidation> _validationFactory;

        public GenericBus(Func<Type, ICommandHandler> commandHandlersFactory, Func<Type, IEnumerable<IEventHandler>> eventHandlersFactory,
            Func<Type, IQueryHandler> queryHandlerFactory, Func<Type, IValidation> validationFactory)
        {
            _commandHandlersFactory = commandHandlersFactory;
            _eventHandlersFactory = eventHandlersFactory;
            _queryHandlerFactory = queryHandlerFactory;
            _validationFactory = validationFactory;
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

        /// <exception cref="ArgumentException"> Command can not be null </exception>
        public void Send<TCommand>(TCommand command) where TCommand : ICommand
        {
            if (command == null)
                throw new ArgumentException("Command can not be null");

            var validator = _validationFactory(typeof(TCommand)) as IValidation<TCommand>;
            validator?.Validate(command);

            var commandHandler = (ICommandHandler<TCommand>)_commandHandlersFactory(typeof(TCommand));
            commandHandler.HandleCommand(command);
        }

        public TResult Send<TResult, TQuery>(TQuery query) where TQuery : IQuery<TResult>
        {
            if (query == null)
                throw new ArgumentException("Query can not be null");

            var validator = _validationFactory(typeof(TQuery)) as IValidation<TQuery>;
            validator?.Validate(query);

            var queryHandler = (IQueryHandler<TResult, TQuery>)_queryHandlerFactory(typeof(TQuery));
            return queryHandler.HandleQuery(query);
        }
    }
}