using Core.CQRS.Commands;
using Core.CQRS.Queries;

namespace Core.CQRS.Bus
{
    /// <summary>
    /// Bus for dispatching commands/queries (facade) 
    /// </summary>
    public interface IBus
    {
        /// <summary>
        /// Command bus 
        /// </summary>
        ICommandBus CommandBus { get; }

        /// <summary>
        /// Query bus 
        /// </summary>
        IQueryBus QueryBus { get; }

        /// <summary>
        /// Sends command over the command bus 
        /// </summary>
        void Send<TCommand>(TCommand command)
            where TCommand : ICommand;

        /// <summary>
        /// Sends query over query bus 
        /// </summary>

        TResult Send<TResult, TQuery>(TQuery query)
            where TQuery : IQuery<TResult>;
    }
}