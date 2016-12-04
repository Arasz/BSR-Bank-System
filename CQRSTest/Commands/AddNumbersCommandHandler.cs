using CQRS.Commands;

namespace CQRSTest.Commands
{
    public class AddNumbersCommandHandler : ICommandHandler<AddNumbersCommand>
    {
        public int Result { get; set; }

        public void HandleCommand(AddNumbersCommand command) => Result = command.A + command.B;
    }
}