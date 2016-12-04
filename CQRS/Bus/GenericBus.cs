using Autofac;
using CQRS.Commands;
using CQRS.Events;
using CQRS.Queries;
using System;

namespace CQRS.Bus
{
    public class GenericBus : IBus
    {
        private readonly IComponentContext _context;

        public GenericBus(IComponentContext context)
        {
            _context = context;
        }

        public void DispatchCommand<TCommand>(TCommand command) where TCommand : ICommand
        {
            if (command == null)
                throw new ArgumentException("Command can not be null");
        }

        public void DispatchEvent<TEvent>(TEvent @event) where TEvent : IEvent
        {
            throw new System.NotImplementedException();
        }

        public TResult DispatchQuery<TQuery, TResult>() where TQuery : IQuery<TResult>
        {
            throw new System.NotImplementedException();
        }
    }
}