using Core.CQRS.Commands;
using Core.CQRS.Queries;

namespace Core.CQRS.Bus
{
    public class GenericBus : IBus
    {
        private readonly ICommandBus _commandBus;

        private readonly IQueryBus _queryBus;

        public ICommandBus CommandBus => _commandBus;

        public IQueryBus QueryBus => _queryBus;

        public GenericBus(ICommandBus commandBus, IQueryBus queryBus)
        {
            _commandBus = commandBus;
            _queryBus = queryBus;
        }

        public void Send<TCommand>(TCommand command) where TCommand : ICommand => _commandBus.Send(command);

        public TResult Send<TResult, TQuery>(TQuery query) where TQuery : IQuery<TResult> => _queryBus.Send<TResult, TQuery>(query);
    }
}