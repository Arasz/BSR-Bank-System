using Core.CQRS.Queries;
using Data.Core;
using Data.Core.Entities;
using Service.Bank.Extensions;
using Service.Bank.Queries;
using System.Data.Entity;
using System.Linq;

namespace Service.Bank.QueryHandlers
{
    public class UserQueryHandler : IQueryHandler<User, UserQuery>
    {
        private readonly BankDataContext _dataContext;

        public UserQueryHandler(BankDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        /// <summary>
        /// Finds user with given user name and password. 
        /// </summary>
        /// <returns> Authenticated user </returns>
        /// <exception cref="InvalidCredentialException"> Wrong user name or password. </exception>
        public User HandleQuery(UserQuery query)
        {
            var authenticatedUser = _dataContext.Users
                                        .Include(user => user.Accounts)
                                        ?.SingleOrDefault(user => user.Name == query.UserName) ?? User.NullUser;

            authenticatedUser.AuthenticateUser(query.UserName, query.Password);

            return authenticatedUser;
        }
    }
}