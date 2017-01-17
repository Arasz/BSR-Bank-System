using CQRS.Commands;
using CQRS.Events;
using CQRS.Queries;

namespace CQRS.Bus
{
    /// <summary>
    /// Bus for dispatching commands/queries/events (facade) 
    /// </summary>
    public interface IBus
    {
        ICommandBus CommandBus { get; }

        IEventBus EventBus { get; }

        IQueryBus QueryBus { get; }

        void Publish<TEvent>(TEvent @event)
            where TEvent : IEvent;

        void Send<TCommand>(TCommand command)
                    where TCommand : ICommand;

        TResult Send<TResult, TQuery>(TQuery query)
            where TQuery : IQuery<TResult>;
    }
}