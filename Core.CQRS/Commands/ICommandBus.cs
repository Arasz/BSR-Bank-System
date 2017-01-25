namespace Core.CQRS.Commands
{
    public interface ICommandBus
    {
        /// <summary>
        /// Sends command to receiver 
        /// </summary>
        /// <typeparam name="TCommand"> Command type </typeparam>
        void Send<TCommand>(TCommand command)
            where TCommand : ICommand;
    }
}