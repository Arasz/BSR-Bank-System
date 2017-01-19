using System.Configuration;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using Core.Common.Configuration;

namespace Service.InterbankTransfer.Authentication
{
    public class CustomUserNamePasswordValidator : UserNamePasswordValidator
    {
        private InterbankTransferConfiguration _interbankTransferConfiguration;

        public CustomUserNamePasswordValidator()
        {
            var path = ConfigurationManager.AppSettings["interbankTransferConfiguration"];
            _interbankTransferConfiguration = InterbankTransferConfiguration.LoadFromFile(path);
        }

        public override void Validate(string userName, string password)
        {
            if (!IsLoginDataValid(userName, password))
                throw new SecurityTokenException("Invalid login or password");
        }

        private bool IsLoginDataValid(string userName, string password)
        {
            return userName == _interbankTransferConfiguration.Login && password == _interbankTransferConfiguration.Password;
        }
    }
}