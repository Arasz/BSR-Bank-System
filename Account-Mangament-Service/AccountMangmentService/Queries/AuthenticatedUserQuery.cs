using CQRS.Queries;
using Data.Core;

namespace Service.Bank.Queries
{
    public class AuthenticatedUserQuery : IQuery<User>
    {
        public string Password { get; }

        public string UserName { get; }

        public AuthenticatedUserQuery(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}