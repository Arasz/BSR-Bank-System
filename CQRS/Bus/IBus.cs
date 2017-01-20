using Core.CQRS.Commands;
using Core.CQRS.Queries;

namespace Core.CQRS.Bus
{
    /// <summary>
    /// Bus for dispatching commands/queries/events (facade) 
    /// </summary>
    public interface IBus
    {
        ICommandBus CommandBus { get; }

        IQueryBus QueryBus { get; }

        void Send<TCommand>(TCommand command)
                    where TCommand : ICommand;

        TResult Send<TResult, TQuery>(TQuery query)
            where TQuery : IQuery<TResult>;
    }
}