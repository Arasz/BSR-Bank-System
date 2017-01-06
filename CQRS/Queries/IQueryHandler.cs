namespace CQRS.Queries
{
    public interface IQueryHandler
    {
    }

    public interface IQueryHandler<out TResult, in TQuery> : IQueryHandler
                where TQuery : IQuery<TResult>
    {
        /// <summary>
        /// Handles given query and returns results 
        /// </summary>
        /// <returns> Query result </returns>
        TResult HandleQuery(TQuery query);
    }
}