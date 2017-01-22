namespace Core.CQRS.Queries
{
    public interface IQueryBus
    {
        /// <summary>
        ///     Sends query to receiver
        /// </summary>
        /// <typeparam name="TResult"> Query result type </typeparam>
        /// <typeparam name="TQuery"> Query type </typeparam>
        /// <returns> Query result </returns>
        TResult Send<TResult, TQuery>(TQuery query)
            where TQuery : IQuery<TResult>;
    }
}