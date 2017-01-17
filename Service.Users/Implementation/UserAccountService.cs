using CQRS.Commands;
using CQRS.Queries;
using Data.Core;
using Service.UserAccount.Commands;
using Service.UserAccount.Contract;
using Service.UserAccount.Queries;

namespace Service.UserAccount.Implementation
{
    public class UserAccountService : IUserAccountService
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;

        public UserAccountService(ICommandBus commandBus, IQueryBus queryBus)
        {
            _commandBus = commandBus;
            _queryBus = queryBus;
        }

        public Account CreateBankAccountForUser(string userName)
        {
            throw new System.NotImplementedException();
        }

        public User CreateUserAccount(string userName, string password)
        {
            _commandBus.Send(new CreateUserCommand(userName, password));
            return _queryBus.Send<User, GetUserQuery>(new GetUserQuery(userName));
        }

        public void DeleteUserAccount(string userName)
        {
            throw new System.NotImplementedException();
        }
    }
}