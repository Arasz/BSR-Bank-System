using System;

namespace CQRS.Queries
{
    public class QueryHandlerKey
    {
        public Type QueryType { get; }

        public Type ResultType { get; }

        public QueryHandlerKey(Type resultType, Type queryType)
        {
            QueryType = queryType;
            ResultType = resultType;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((QueryHandlerKey)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((QueryType != null ? QueryType.GetHashCode() : 0) * 397) ^ (ResultType != null ? ResultType.GetHashCode() : 0);
            }
        }

        protected bool Equals(QueryHandlerKey other)
        {
            return Equals(QueryType, other.QueryType) && Equals(ResultType, other.ResultType);
        }
    }
}