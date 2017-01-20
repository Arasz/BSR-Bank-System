using Core.CQRS.Commands;
using Core.CQRS.Queries;

namespace Core.CQRS.Bus
{
    public class GenericBus : IBus
    {
        public ICommandBus CommandBus { get; }

        public IQueryBus QueryBus { get; }

        public GenericBus(ICommandBus commandBus, IQueryBus queryBus)
        {
            CommandBus = commandBus;
            QueryBus = queryBus;
        }

        public void Send<TCommand>(TCommand command) where TCommand : ICommand => CommandBus.Send(command);

        public TResult Send<TResult, TQuery>(TQuery query) where TQuery : IQuery<TResult>
            => QueryBus.Send<TResult, TQuery>(query);
    }
}