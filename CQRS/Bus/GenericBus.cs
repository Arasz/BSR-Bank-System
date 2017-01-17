using CQRS.Commands;
using CQRS.Events;
using CQRS.Queries;

namespace CQRS.Bus
{
    public class GenericBus : IBus
    {
        private readonly ICommandBus _commandBus;
        private readonly IEventBus _eventBus;
        private readonly IQueryBus _queryBus;

        public ICommandBus CommandBus => _commandBus;

        public IEventBus EventBus => _eventBus;

        public IQueryBus QueryBus => _queryBus;

        public GenericBus(ICommandBus commandBus, IEventBus eventBus, IQueryBus queryBus)
        {
            _commandBus = commandBus;
            _eventBus = eventBus;
            _queryBus = queryBus;
        }

        public void Publish<TEvent>(TEvent @event) where TEvent : IEvent => _eventBus.Publish(@event);

        public void Send<TCommand>(TCommand command) where TCommand : ICommand => _commandBus.Send(command);

        public TResult Send<TResult, TQuery>(TQuery query) where TQuery : IQuery<TResult> => _queryBus.Send<TResult, TQuery>(query);
    }
}