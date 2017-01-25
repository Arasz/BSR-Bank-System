using Core.CQRS.Queries;
using Data.Core.Entities;

namespace Service.Bank.Queries
{
    /// <summary>
    /// Query for user with given user name and password 
    /// </summary>
    public class UserQuery : IQuery<User>
    {
        public string Password { get; }

        public string UserName { get; }

        public UserQuery(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}