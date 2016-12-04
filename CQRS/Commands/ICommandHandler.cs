namespace CQRS.Commands
{
    public interface ICommandHandler
    {
    }

    public interface ICommandHandler<in TCommand> : ICommandHandler
        where TCommand : ICommand
    {
        /// <summary>
        /// Executes action for given command 
        /// </summary>
        void HandleCommand(TCommand command);
    }
}