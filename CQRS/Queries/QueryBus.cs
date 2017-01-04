using CQRS.Exceptions;
using System;

namespace CQRS.Queries
{
    public class QueryBus : IQueryBus
    {
        private readonly Func<QueryHandlerKey, IQueryHandler> _queryHandlerFactory;

        public QueryBus(Func<QueryHandlerKey, IQueryHandler> queryHandlerFactory)
        {
            _queryHandlerFactory = queryHandlerFactory;
        }

        /// <exception cref="NullHandlerException">
        /// Factory method returned null command handler
        /// </exception>
        /// <exception cref="ArgumentException"> Query can not be null </exception>
        public TResult Send<TResult, TQuery>(TQuery query) where TQuery : IQuery<TResult>
        {
            if (query == null)
                throw new ArgumentException("Query can not be null");

            var queryHandler = (IQueryHandler<TResult, TQuery>)_queryHandlerFactory(new QueryHandlerKey(typeof(TResult), typeof(TQuery)));

            if (queryHandler == null)
                throw new NullHandlerException("Factory method returned null query handler", typeof(IQueryHandler<TResult, TQuery>));

            return queryHandler.HandleQuery(query);
        }
    }
}