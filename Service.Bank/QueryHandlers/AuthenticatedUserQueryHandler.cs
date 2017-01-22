using System.Data.Entity;
using System.Linq;
using Core.CQRS.Queries;
using Data.Core;
using Service.Bank.Extensions;
using Service.Bank.Queries;

namespace Service.Bank.QueryHandlers
{
    public class AuthenticatedUserQueryHandler : IQueryHandler<User, AuthenticatedUserQuery>
    {
        private readonly BankDataContext _dataContext;

        public AuthenticatedUserQueryHandler(BankDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public User HandleQuery(AuthenticatedUserQuery query)
        {
            var authenticatedUser = _dataContext.Users
                                        .Include(user => user.Accounts)
                                        .SingleOrDefault(user => user.Name == query.UserName) ?? User.NullUser;

            authenticatedUser.AuthenticateUser(query.UserName, query.Password);

            return authenticatedUser;
        }
    }
}