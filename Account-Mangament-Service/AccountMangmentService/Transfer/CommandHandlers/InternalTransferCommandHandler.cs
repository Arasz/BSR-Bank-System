using CQRS.Commands;
using Service.Bank.Transfer.Commands;

namespace Service.Bank.Transfer.CommandHandlers
{
    public class InternalTransferCommandHandler : ICommandHandler<InternalTransferCommand>
    {
        public void HandleCommand(InternalTransferCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}