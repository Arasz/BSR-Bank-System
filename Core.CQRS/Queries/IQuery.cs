namespace Core.CQRS.Queries
{
    /// <summary>
    /// Marker interface for query 
    /// </summary>
    /// <typeparam name="TResult"> Query result type </typeparam>
    public interface IQuery<TResult>
    {
    }
}