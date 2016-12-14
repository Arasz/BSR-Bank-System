using AccountMangmentService.Commands;
using CQRS.Commands;

namespace AccountMangmentService.CommandHandlers
{
    public class InternalPaymentCommandHandler : ICommandHandler<InternalPaymentCommand>
    {
        public void HandleCommand(InternalPaymentCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}