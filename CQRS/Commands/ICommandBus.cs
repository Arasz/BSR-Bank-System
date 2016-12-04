namespace CQRS.Commands
{
    public interface ICommandBus
    {
        void Send<TCommand>(TCommand command)
            where TCommand : ICommand;
    }
}