namespace Core.CQRS.Commands
{
    public interface ICommandHandler
    {
    }

    public interface ICommandHandler<in TCommand> : ICommandHandler
        where TCommand : ICommand
    {
        /// <summary>
        /// Handles given commands (can modify state) 
        /// </summary>
        void HandleCommand(TCommand command);
    }
}