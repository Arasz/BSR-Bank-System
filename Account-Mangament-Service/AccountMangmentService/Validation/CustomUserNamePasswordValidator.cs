using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Linq;
using System.ServiceModel;
using Core.Common.Security;
using Data.Core;

namespace Service.Bank.Validation
{
    public class CustomUserNamePasswordValidator : UserNamePasswordValidator
    {
        private readonly IPasswordHasher _passwordHasher;
        private BankDataContext _dataContext;

        public CustomUserNamePasswordValidator() : this(new BankDataContext(), new DefaultPasswordHasher())
        {
        }

        public CustomUserNamePasswordValidator(BankDataContext dataContext, IPasswordHasher passwordHasher)
        {
            _dataContext = dataContext;
            _passwordHasher = passwordHasher;
        }

        /// <exception cref="SecurityTokenException"> Correct user name and password required </exception>
        public override void Validate(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
                throw new SecurityTokenException("Login and password required");

            var authenticatedUser = _dataContext.Users.SingleOrDefault(user => user.Name == userName) ?? User.NullUser;

            authenticatedUser.AuthenticateUser(userName, password, _passwordHasher);
        }
    }
}