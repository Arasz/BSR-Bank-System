using Core.Common.Configuration;
using System.Configuration;
using System.IdentityModel.Selectors;
using System.Security.Authentication;

namespace Service.InterbankTransfer.Authentication
{
    public class CustomUserNamePasswordValidator : UserNamePasswordValidator
    {
        private InterbankTransferConfiguration _interbankTransferConfiguration;

        public override void Validate(string userName, string password)
        {
            LoadConfiguration();

            if (!IsLoginDataValid(userName, password))
                throw new InvalidCredentialException("Invalid login or password");
        }

        private bool IsLoginDataValid(string userName, string password) => userName == _interbankTransferConfiguration.Login &&
                                                                           password == _interbankTransferConfiguration.Password;

        private void LoadConfiguration()
        {
            if (_interbankTransferConfiguration != null)
                return;
            var path = ConfigurationManager.AppSettings["interbankTransferConfiguration"];
            _interbankTransferConfiguration = InterbankTransferConfiguration.LoadFromFile(path);
        }
    }
}