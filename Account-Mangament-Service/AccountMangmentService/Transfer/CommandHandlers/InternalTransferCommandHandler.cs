using BankService.Transfer.Commands;
using CQRS.Commands;

namespace BankService.Transfer.CommandHandlers
{
    public class InternalTransferCommandHandler : ICommandHandler<InternalTransferCommand>
    {
        public void HandleCommand(InternalTransferCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}