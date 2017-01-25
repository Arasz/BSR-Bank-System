namespace Core.CQRS.Commands
{
    /// <summary>
    /// Marker interface for command handler 
    /// </summary>
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