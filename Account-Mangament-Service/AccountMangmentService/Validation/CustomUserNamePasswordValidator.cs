using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Linq;
using System.ServiceModel;
using Data.Core;

namespace Service.Bank.Validation
{
    public class CustomUserNamePasswordValidator : UserNamePasswordValidator
    {
        private BankDataContext _dataContext = new BankDataContext();

        /// <exception cref="SecurityTokenException"> Correct user name and password required </exception>
        public override void Validate(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
                throw new SecurityTokenException("Login and password required");

            var authenticatedUser = _dataContext.Users.SingleOrDefault(user => user.Name == userName) ?? User.NullUser;

            authenticatedUser.AuthenticateUser(userName, password);
        }

        private void ThrowFaultException<TException>(TException exception)
        {
            throw new FaultException<TException>(exception);
        }
    }
}