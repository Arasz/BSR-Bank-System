using CQRS.Commands;
using CQRS.Events;
using CQRS.Queries;

namespace CQRS.Bus
{
    /// <summary>
    /// Bus for dispatching commands/queries/events 
    /// </summary>
    public interface IBus
    {
        void DispatchCommand<TCommand>(TCommand command)
            where TCommand : ICommand;

        void DispatchEvent<TEvent>(TEvent @event)
            where TEvent : IEvent;

        TResult DispatchQuery<TQuery, TResult>()
            where TQuery : IQuery<TResult>;
    }
}