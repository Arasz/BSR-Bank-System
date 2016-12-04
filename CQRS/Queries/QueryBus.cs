using System;

namespace CQRS.Queries
{
    internal class QueryBus : IQueryBus
    {
        private readonly Func<Type, IQueryHandler> _queryHandlerFactory;

        public QueryBus(Func<Type, IQueryHandler> queryHandlerFactory)
        {
            _queryHandlerFactory = queryHandlerFactory;
        }

        public TResult Send<TResult, TQuery>(TQuery query) where TQuery : IQuery<TResult>
        {
            if (query == null)
                throw new ArgumentException("Query can not be null");

            var queryHandler = (IQueryHandler<TResult, TQuery>)_queryHandlerFactory(typeof(TQuery));
            return queryHandler.HandleQuery(query);
        }
    }
}