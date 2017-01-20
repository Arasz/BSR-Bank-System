using Core.CQRS.Queries;
using Data.Core;

namespace Service.UserAccount.Queries
{
    public class GetUserQuery : IQuery<User>
    {
        public string UserName { get; set; }

        public GetUserQuery(string userName)
        {
            UserName = userName;
        }
    }
}