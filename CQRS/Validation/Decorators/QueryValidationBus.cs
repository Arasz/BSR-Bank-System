using CQRS.Queries;
using System;

namespace CQRS.Validation.Decorators
{
    /// <summary>
    /// Query bus decorator. Validates query before send. 
    /// </summary>
    public class QueryValidationBus : IQueryBus
    {
        private readonly IQueryBus _queryBus;
        private readonly Func<Type, IValidation> _validationFactory;

        public QueryValidationBus(IQueryBus queryBus, Func<Type, IValidation> validationFactory)
        {
            _queryBus = queryBus;
            _validationFactory = validationFactory;
        }

        public TResult Send<TResult, TQuery>(TQuery query) where TQuery : IQuery<TResult>
        {
            var validator = _validationFactory(typeof(TQuery)) as IValidation<TQuery>;

            validator?.Validate(query);

            return _queryBus.Send<TResult, TQuery>(query);
        }
    }
}