using CQRS.Commands;

namespace Service.Bank.Commands
{
    public class RegisterOperationCommand<TCommand> : ICommand
        where TCommand : TransferCommand
    {
        public decimal AccountBalance { get; }

        public TCommand ExecutedCommand { get; }

        public RegisterOperationCommand(TCommand executedCommand, decimal accountBalance)
        {
            ExecutedCommand = executedCommand;
            AccountBalance = accountBalance;
        }
    }
}