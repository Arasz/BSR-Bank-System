using CQRS.Commands;
using Service.Bank.Commands;

namespace Service.Bank.CommandHandlers
{
    public class InternalTransferCommandHandler : ICommandHandler<InternalTransferCommand>
    {
        public void HandleCommand(InternalTransferCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}