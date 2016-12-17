using AccountMangmentService.Transfer.Commands;
using CQRS.Commands;

namespace AccountMangmentService.Transfer.CommandHandlers
{
    public class InternalTransferCommandHandler : ICommandHandler<InternalTransferCommand>
    {
        public void HandleCommand(InternalTransferCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}