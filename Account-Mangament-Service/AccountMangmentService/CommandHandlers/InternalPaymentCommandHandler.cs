using AccountMangmentService.Commands;
using CQRS.Commands;

namespace AccountMangmentService.CommandHandlers
{
    public class InternalPaymentCommandHandler : ICommandHandler<InternalTransferCommand>
    {
        public void HandleCommand(InternalTransferCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}