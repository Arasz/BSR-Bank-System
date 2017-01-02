using AccountMangementService.Transfer.Commands;
using CQRS.Commands;

namespace AccountMangementService.Transfer.CommandHandlers
{
    public class InternalTransferCommandHandler : ICommandHandler<InternalTransferCommand>
    {
        public void HandleCommand(InternalTransferCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}