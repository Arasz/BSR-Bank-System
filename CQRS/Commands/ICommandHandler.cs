namespace CQRS.Commands
{
    public interface ICommandHandler<in TCommand>
        where TCommand : ICommand
    {
        /// <summary>
        /// Executes action for given command 
        /// </summary>
        void HandleCommand(TCommand command);
    }
}