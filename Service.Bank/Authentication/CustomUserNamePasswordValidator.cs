using Core.Common.Security;
using Data.Core;
using Service.Bank.Extensions;
using System.IdentityModel.Selectors;
using System.Linq;
using System.Security.Authentication;

namespace Service.Bank.Authentication
{
    public class CustomUserNamePasswordValidator : UserNamePasswordValidator
    {
        private readonly BankDataContext _dataContext;
        private readonly IPasswordHasher _passwordHasher;

        public CustomUserNamePasswordValidator() : this(new BankDataContext(), new DefaultPasswordHasher())
        {
        }

        public CustomUserNamePasswordValidator(BankDataContext dataContext, IPasswordHasher passwordHasher)
        {
            _dataContext = dataContext;
            _passwordHasher = passwordHasher;
        }

        public override void Validate(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
                throw new InvalidCredentialException("Empty user name or password");

            var authenticatedUser = _dataContext.Users.SingleOrDefault(user => user.Name == userName) ?? User.NullUser;

            authenticatedUser.AuthenticateUser(userName, password, _passwordHasher);
        }
    }
}