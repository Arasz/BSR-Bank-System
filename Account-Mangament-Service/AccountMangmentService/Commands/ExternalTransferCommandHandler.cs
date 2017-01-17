using CQRS.Commands;

namespace Service.Bank.Commands
{
    public class ExternalTransferCommandHandler : ICommandHandler<ExternalTransferCommand>
    {
        public void HandleCommand(ExternalTransferCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}