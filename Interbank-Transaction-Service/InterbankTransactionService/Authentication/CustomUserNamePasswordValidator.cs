using System.IdentityModel.Selectors;

namespace Service.InterbankTransfer.Authentication
{
    public class CustomUserNamePasswordValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            throw new System.NotImplementedException();
        }
    }
}