using System.Collections.Generic;
using CQRS.Commands;
using Data.Core;
using Shared.Security;

namespace Service.UserAccount.Commands
{
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
    {
        private readonly BankDataContext _dataContext;
        private readonly IPasswordHasher _passwordHasher;

        public CreateUserCommandHandler(IPasswordHasher passwordHasher, BankDataContext dataContext)
        {
            _passwordHasher = passwordHasher;
            _dataContext = dataContext;
        }

        public void HandleCommand(CreateUserCommand command)
        {
            var passwordHash = _passwordHasher.HashPassword(command.Password);

            var newUser = new User
            {
                Name = command.UserName,
                Password = passwordHash,
                Accounts = new List<Account>()
            };

            _dataContext.Users.Add(newUser);
        }
    }
}