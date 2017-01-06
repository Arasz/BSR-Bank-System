using CQRS.Commands;
using Service.Bank.Commands;

namespace Service.Bank.CommandHandlers
{
    public class WithdrawCommandHandler : ICommandHandler<WithdrawCommand>
    {
        public void HandleCommand(WithdrawCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}