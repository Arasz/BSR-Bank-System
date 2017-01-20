using System.Linq;
using Core.CQRS.Queries;
using Data.Core;

namespace Service.UserAccount.Queries
{
    public class GetUserQueryHandler : IQueryHandler<User, GetUserQuery>

    {
        private readonly BankDataContext _dataContext;

        public GetUserQueryHandler(BankDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public User HandleQuery(GetUserQuery query) => _dataContext.Users.Single(user => user.Name == query.UserName);
    }
}