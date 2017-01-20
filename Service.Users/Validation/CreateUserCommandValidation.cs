using System.Linq;
using Data.Core;
using FluentValidation;
using Service.UserAccount.Commands;

namespace Service.UserAccount.Validation
{
    public class CreateUserCommandValidation : AbstractValidator<CreateUserCommand>
    {
        private readonly BankDataContext _dataContext;

        public CreateUserCommandValidation(BankDataContext dataContext)
        {
            _dataContext = dataContext;

            RuleFor(command => command.UserName)
                .NotEmpty()
                .Length(1, 30)
                .Must(IsUserNameUnique);

            RuleFor(command => command.Password)
                .NotEmpty()
                .Length(1, 30)
                .Matches(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{4,}$")
                .WithMessage("Password must have at last 4 characters, one digit and one upper case letter");
        }

        private bool IsUserNameUnique(string userName) => !_dataContext.Users.Any(user => user.Name == userName);
    }
}