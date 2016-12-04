namespace CQRS.Queries
{
    public interface IQueryBus
    {
        TResult Send<TResult, TQuery>(TQuery query)
            where TQuery : IQuery<TResult>;
    }
}