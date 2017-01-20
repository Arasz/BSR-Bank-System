using ICommand = Core.CQRS.Commands.ICommand;

namespace Service.UserAccount.Commands
{
    public class CreateUserCommand : Core.CQRS.Commands.ICommand
    {
        public string Password { get; private set; }

        public string UserName { get; private set; }

        public CreateUserCommand(string userName, string password)
        {
            Password = password;
            UserName = userName;
        }
    }
}