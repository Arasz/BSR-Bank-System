namespace CQRS.Queries
{
    public interface IQueryHandler
    {
    }

    public interface IQueryHandler<out TResult, in TQuery> : IQueryHandler
    {
        TResult HandleQuery(TQuery query);
    }
}